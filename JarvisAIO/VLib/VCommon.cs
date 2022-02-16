using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisAIO.VLib
{
    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.Utility;
    using SharpDX;

    public class VCommon
    {
        private static AIHeroClient Player { get { return ObjectManager.Player; } }
        private static List<UnitIncomingDamage> IncomingDamageList = new List<UnitIncomingDamage>();
        private static List<Vector3> drawMinionCircles = new List<Vector3>();
        private static List<AIMinionClient> drawMinions = new List<AIMinionClient>();

        public static float GetKsDamage(AIHeroClient target, Spell qwer, bool includeIncomingDamage = true)
        {
            var totalDamage = qwer.GetDamage(target) - target.AllShield - target.HPRegenRate;
            if(totalDamage > target.Health)
            {
                //블츠 패시브가 있을 경우
                //마나 비례 쉴드량을 계산하고 다시 데미지를 계산
                if (target.CharacterName == "Blitzcrank"
                    && !target.HasBuff("manabarrier")
                    && !target.HasBuff("manabarriercooldown"))
                    totalDamage -= 0.3f * target.MaxMana;
            }

            if (includeIncomingDamage)
                totalDamage += (float)GetIncomingDamage(target);

            return totalDamage;
        }

        public static bool CheckMinionCollision(AIBaseClient unit, Vector3 Pos, float delay, float radius, float speed,
                                                 Vector3 from, float range)
        {
            List<AIMinionClient> minions = new List<AIMinionClient>();
            foreach (AIMinionClient minion in ObjectManager.Get<AIMinionClient>())
            {
                if (minion.Team != ObjectManager.Player.Team &&
                    Utils.GetDistance(from, minion.Position) < range + 500 * (delay + range / speed))
                {
                    if (minion.IsValid)
                        minions.Add(minion);
                }
            }

            drawMinions = minions;
            if (CheckCollision(unit, minions, delay, radius, speed, from, range))
            {
                return true;
            }
            return false;
        }

        public static bool CheckCollision(AIBaseClient unit, List<AIMinionClient> minions, float delay, float radius, float speed, Vector3 from, float range)
        {
            List<Vector3> minionCircles = new List<Vector3>();
            for (int i = 0; i < minions.Count(); i++)
            {
                AIMinionClient minion = minions[i];
                if (minion.IsValid)
                {
                    List<Vector3> waypoints = GetWayPoints(minion);
                    Vector3 castPosition = CalculateTargetPosition(minion, delay, radius, speed, from);
                    Vector3 castPositionHero = CalculateTargetPosition(unit, delay, radius, speed, from);

                    minionCircles.Add(castPosition);
                    drawMinionCircles = minionCircles;

                    if (Utils.GetDistanceSqr(from, castPosition) <= Math.Pow(range, 2) &&
                        Utils.GetDistanceSqr(from, minion.Position) <= Math.Pow(range + 100, 2))
                    {
                        Vector3 pos = new Vector3();
                        if (minion.IsMoving)
                        {
                            pos = castPosition;
                        }
                        else
                        {
                            pos = minion.Position;
                        }
                        Vector3 posHero = new Vector3();
                        if (unit.IsMoving)
                        {
                            posHero = castPositionHero;
                        }
                        else
                        {
                            posHero = unit.Position;
                        }
                        if (waypoints.Count > 1 && Utils.IsValidVector3(waypoints[0]) &&
                            Utils.IsValidVector3(waypoints[1]))
                        {
                            Object[] objects1 = Utils.VectorPointProjectionOnLineSegment(from, posHero, pos);
                            Vector3 pointSegment1 = (Vector3)objects1[0];
                            Vector3 pointLine1 = (Vector3)objects1[1];
                            bool isOnSegment1 = (bool)objects1[2];
                            if (isOnSegment1 &&
                                (Utils.GetDistanceSqr(pos, pointSegment1) <=
                                 Math.Pow(GetHitBox(minion) + radius + 20, 2)))
                            {
                                return true;
                            }
                        }
                        Object[] objects = Utils.VectorPointProjectionOnLineSegment(from, posHero, pos);
                        Vector3 pointSegment = (Vector3)objects[0];
                        Vector3 pointLine = (Vector3)objects[1];
                        bool isOnSegment = (bool)objects[2];
                        if (isOnSegment &&
                            (Utils.GetDistanceSqr(pos, pointSegment) <= Math.Pow(GetHitBox(minion) + radius + 20, 2)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static float GetHitBox(AIBaseClient object1)
        {
            return object1.BoundingRadius;
        }

        private static Vector3 CalculateTargetPosition(AIBaseClient hero, float delay, float radius,
                                                       float speed, Vector3 from)
        {
            var castPosition = new Vector3();
            List<Vector3> waypoints = GetWayPoints(hero);
            if (waypoints == null)
                return new Vector3();
            double wayPointsLength = GetWayPointsLength(waypoints);
            if (waypoints.Count() == 1)
            {
                castPosition = new Vector3(waypoints[0].X, waypoints[0].Y, waypoints[0].Z);
            }
            else if (wayPointsLength - delay * hero.MoveSpeed + radius >= 0)
            {
                waypoints = CutWayPoints(waypoints, delay * hero.MoveSpeed - radius);
                if (!float.IsNaN(speed) && speed.CompareTo(float.MaxValue) != 0)
                {
                    for (int i = 0; i < waypoints.Count() - 1; i++)
                    {
                        Vector3 waypoint1 = waypoints[i];
                        Vector3 waypoint2 = waypoints[i + 1];
                        if (!Utils.IsValidVector3(waypoint1) || !Utils.IsValidVector3(waypoint2))
                            continue;
                        castPosition = waypoint1;
                    }
                }
                else
                {
                    castPosition = new Vector3(waypoints[waypoints.Count() - 1].X, waypoints[waypoints.Count() - 1].Y,
                                               waypoints[waypoints.Count() - 1].Z);
                }
            }
            else if (hero.Type != ObjectManager.Player.Type)
            {
                castPosition = new Vector3(waypoints[waypoints.Count() - 1].X, waypoints[waypoints.Count() - 1].Y,
                                           waypoints[waypoints.Count() - 1].Z);
            }

            return castPosition;
        }

        

        private static List<Vector3> CutWayPoints(List<Vector3> waypoints, float distance)
        {
            var result = new List<Vector3>();

            double remaining = distance;
            if (distance > 0)
            {
                for (int i = 0; i < waypoints.Count(); i++)
                {
                    Vector3 waypoint1 = waypoints[i];
                    Vector3 waypoint2 = waypoints[i + 1];
                    double nDistance = Utils.GetDistance(waypoint1, waypoint2);
                    if (nDistance >= remaining)
                    {
                        Vector3 temp = (waypoint2 - waypoint1);
                        temp.Normalize();
                        Vector3.Multiply(ref temp, (float)remaining, out temp);
                        result.Add(waypoint1 + temp);


                        for (int j = i; j < waypoints.Count() - 1; j++)
                        {
                            result.Add(waypoints[j]);
                        }
                        remaining = 0;
                        break;
                    }
                    else
                    {
                        remaining = remaining - nDistance;
                    }
                }
            }
            else
            {
                if (waypoints == null || waypoints.Count() == 0)
                    return result;
                Vector3 waypoint1 = waypoints[0];
                Vector3 waypoint2 = waypoints[0 + 1];
                result = waypoints;
                Vector3 temp = (waypoint2 - waypoint1);
                temp.Normalize();
                Vector3.Multiply(ref temp, (float)remaining, out temp);
                result.Add(waypoint1 + temp);
            }
            return result;
        }

        private static double GetWayPointsLength(List<Vector3> waypoints)
        {
            double result = 0f;
            for (int i = 0; i < waypoints.Count() - 1; i++)
            {
                Vector3 distance1 = waypoints[i];
                Vector3 distance2 = waypoints[i + 1];
                double distance = Utils.GetDistance(distance1, distance2);
                result = result + distance;
            }
            return result;
        }

        private static List<Vector3> GetWayPoints(AIBaseClient unit)
        {
            var pathes = new List<Vector3>();
            var pathes2 = new List<Vector3>();
            foreach (var h in ObjectManager.Get<AIHeroClient>())
            {
                if(h.NetworkId == unit.NetworkId)
                {
                    pathes.AddRange(unit.Path);
                    break;
                }
            }
            pathes2.Add(unit.Position);
            pathes2.AddRange(pathes);
            return pathes2;
        }

        public static double GetIncomingDamage(AIHeroClient target, float time = 0.5f, bool skillshots = true)
        {
            var totalDamage = 0d;

            foreach(var damage in IncomingDamageList.Where(
                d => d.TargetNetworkId == target.NetworkId && Game.Time - time < d.Time))
            {
                if (skillshots)
                    totalDamage += damage.Damage;
                else if (!damage.Skillshot)
                    totalDamage += damage.Damage;
            }

            if (target.HasBuffOfType(BuffType.Poison))
                totalDamage += target.Level * 5;
            if (target.HasBuffOfType(BuffType.Damage))
                totalDamage += target.Level * 6;

            return totalDamage;
        }

        public static bool CanMove(AIHeroClient target)
            => !((!target.IsWindingUp && !target.CanMove)
            || target.MoveSpeed < 50
            || target.HaveImmovableBuff());

        public static bool CanHarass()
            => !Player.IsWindingUp && !Player.IsUnderEnemyTurret() && Orbwalker.CanMove(50, false);


        class UnitIncomingDamage
        {
            public double Damage { get; set; }
            public bool Skillshot { get; set; }
            public int TargetNetworkId { get; set; }
            public float Time { get; set; }
        }
    }
}

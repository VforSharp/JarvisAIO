using EnsoulSharp.SDK;
using JarvisAIO.VLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisAIO.Champions.Ezreal.Modes
{
    class Combo : Base
    {
        public static void CastQ()
        {
            if (!Q.IsReady()) return;

            if (Program.LagFree(1))
            {
                foreach (var t in GameObjects.EnemyHeroes.Where(enemy => enemy.IsValidTarget(Q.Range)).OrderBy(t => t.Health))
                {
                    //Q 1방에 죽을 적이 있다면 자동 Q스킬 사용
                    if (VCommon.GetKsDamage(t, Q) > t.Health)
                        Program.CastSpell(Q, t);
                    //스턴에 걸린 적 자동 Q스킬 사용
                    else if(t.IsStunned)
                        Program.CastSpell(Q, t);
                }

                var ts = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                Program.CastSpell(Q, ts);
            }
        }

        public static void CastW()
        {
            if (Program.LagFree(0))
            {
                var ts = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                
                //딸피인 적한테 W 사용 안함
                var checkLowHP = VCommon.GetKsDamage(ts, Q) > ts.Health;
                if (Player.Mana > QMANA + WMANA && !checkLowHP)
                {
                    var CanCastQ = Q.GetPrediction(ts).CollisionObjects.FirstOrDefault() == null;

                    //Q가 미니언에 안막히는 상태라면 W 사용
                    //W 투사체 속도 계산해서 상대 이속 뺀 거리에서만 사용
                    if (ts.IsValidTarget(Q.Range - ts.MoveSpeed) && CanCastQ && Q.IsReady() && W.IsReady())
                        Program.CastSpell(W, ts);

                    //평타 사거리 안에 있으면 W평 콤보 유도
                    else if(ts.IsValidTarget(Player.AttackRange - Player.AttackDelay) && W.IsReady())
                        Program.CastSpell(W, ts);
                }
            }
        }



    }
}

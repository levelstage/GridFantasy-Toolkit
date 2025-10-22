using GfEngine.Battles.Commands.Core;
using GfToolkit.Shared;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace GfEngine.Battles.Commands.Advanced
{
    public class BasicAttackCommand : HitCommand
    {
        public List<Command> Incidents { get; set; }
        public HitCommand CounterAttackCommand { get; set; }
        public bool HadInitiative { get; set; } // 행위자가 선공하는데 성공했는지(false면 반격자 선공)
    }
}
using System.Collections.Generic;
using System.Linq;
using GfEngine.Battles.Commands;
using GfToolkit.Shared;

namespace GfEngine.Logics
{
    public class CommandSchedular
    {
        private readonly LinkedList<Command> _commandQueue = new LinkedList<Command>();
        
        public void Enqueue(Command command)
        {
            _commandQueue.AddLast(command);
        }
        public void Interrupt(IEnumerable<Command> commands)
        {
            var sortedCommands = commands
                // 1차 정렬: CommandPriority (ExecutionPriority)를 내림차순 (높은 숫자가 먼저 실행)
                .OrderByDescending(cmd => cmd.ExecutionPriority)
                
                // 2차 정렬: 1차 기준이 같을 경우, SourceUnit의 속도를 오름차순 (느린 순)
                // (느린 순으로 정렬해야 AddFirst 시 빠른 것이 맨 위에 쌓입니다.)
                .ThenBy(cmd => 
                {
                    // Null 방어 로직: SourceUnit이 없으면 가장 낮은 속도(int.MinValue)로 간주
                    if (cmd.SourceUnit == null)
                    {
                        // 시스템 명령은 ExecutionPriority가 이미 낮으므로, Agility는 최소값 부여
                        return int.MinValue; 
                    }
                    return cmd.SourceUnit.ParseStat(StatType.Agility); 
                })
                .ToList();

            // 느린 순으로 정렬된 리스트를 순서대로 큐의 맨 앞에 삽입 (AddFirst)
            foreach (var command in sortedCommands) 
            {
                _commandQueue.AddFirst(command);
            }
        }
        public Command Dequeue()
        {
            if (_commandQueue.Count == 0) return null;
            Command command = _commandQueue.First.Value;
            _commandQueue.RemoveFirst();
            return command;
        }
    }
}
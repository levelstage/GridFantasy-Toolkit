using GfToolkit.Shared.Dtos.Models;
using GfToolkit.Shared;
using GfStudio.Dialogues;
namespace GfStudio.Pages
{
    public partial class BuffSetEditor
    {
        private BuffSetDto _selectedBuffSet;
        private List<BuffSetDto> _buffSets = new List<BuffSetDto>{
            new BuffSetDto(){
            Code = 0,
            Name = "임시",Description = "임시입니당.",
            Duration = -1,
            Effects = new List<BuffDto>()
            {
                new BuffDto()
                {
                    Type = BuffEffect.MaxHpBoost,
                    Magnitude = 10
                },
                new AuraDto()
                {
                    Type = BuffEffect.Aura,
                    Magnitude = 0,
                    AuraScope = BasicPatternType.King,
                    UseAttackPattern = false,
                    UseMovePattern = false,
                    AuraBuffCode = 1

                },
                new ScalingBuffDto()
                {
                    Type = BuffEffect.AttackBoost,
                    ScaleFactor = 0.1f,
                    TargetStat = StatType.Attack
                }
            },
            IsBuff = false,IsDebuff=false,IsRemovable=false,IsVisible=false
            }
        };
        private void HandleBuffSetSelected(BuffSetDto selected)
        {
            _selectedBuffSet = selected;
        }
        private async Task OpenAddEffectDialog()
        {
            var dialogReference = await DialogService.ShowAsync<EffectEditorDialog>("Add New Effect");
            var result = await dialogReference.Result;
            if(result != null && !result.Canceled)
            {
                var newEffect = result.Data as BuffDto;
                _selectedBuffSet.Effects.Add(newEffect);
                StateHasChanged();
            }
        }
    }
}
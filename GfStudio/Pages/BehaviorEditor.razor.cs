using GfToolkit.Shared.Dtos.Models;
using GfToolkit.Shared.Dtos;
using GfToolkit.Shared;
using GfStudio.Dialogs;
using MudBlazor;
using System.Reflection.Metadata;
using GfToolkit.Shared.Dtos.Behaviors;
namespace GfStudio.Pages
{
    public partial class BehaviorEditor
    {
        private readonly string[] _behaviorTypes = 
        {
            "AreaAttack", "SelfEffect"
        };
        private List<BehaviorDto> _behaviors { get; set; } = GameDataDto.Database.Behaviors;
        private BehaviorDto _selectedBehavior { get; set; }
        private IEnumerable<BasicPatternType> _patternTypeOptions = Enum.GetValues<BasicPatternType>();
        private IEnumerable<DamageType> _damageTypeOptions = Enum.GetValues<DamageType>();
        protected override void OnInitialized()
        {
            _selectedBehavior = _behaviors.FirstOrDefault();
        }
        private void HandleBehaviorSelected(BehaviorDto selected)
        {
            _selectedBehavior = selected;
        }
        private async Task AreaAttack_PickBuffSet()
        {
            var parameters = new DialogParameters<BuffSetPickerDialog>
            {
                { x => x.AllBuffSets, GameDataDto.Database.BuffSets}, // Database의 모든 BuffSet 전달.
            };
            var dialog = await DialogService.ShowAsync<BuffSetPickerDialog>("Select Buffset", parameters);
            var result = await dialog.Result;
            if (result != null && !result.Canceled && _selectedBehavior is AreaAttackBehaviorDto)
            {
                (_selectedBehavior as AreaAttackBehaviorDto).ApplyingBuffSetCode = ((BuffSetDto)result.Data).Code;
                StateHasChanged();
            }
        }
        private async Task OpenChangeMaxDialog()
        {
            var parameters = new DialogParameters<ChangeMaxDialog>
            {
                { x => x._oldMaximum, _behaviors.Count }, // 기존 maximum 전달.
            };
            var dialog = await DialogService.ShowAsync<ChangeMaxDialog>("Change Maximum Value", parameters);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                var newMax = (int)result.Data;
                var currentCount = _behaviors.Count;

                // 새로운 최대치가 현재 개수보다 많으면, 빈 항목을 추가합니다.(가장 단순한 SelfEffectBehavior로.)
                if (newMax > currentCount)
                {
                    for (int i = currentCount; i < newMax; i++)
                    {
                        _behaviors.Add(new SelfEffectBehaviorDto { Code = i, Name = "" });
                    }
                }
                // 새로운 최대치가 현재 개수보다 적으면, 뒤에서부터 항목을 삭제합니다.
                else if (newMax < currentCount)
                {
                    _behaviors.RemoveRange(newMax, currentCount - newMax);
                }

                StateHasChanged(); // 리스트가 변경되었으니 화면을 새로고침합니다.
            }
        }

        private void OnBehaviorTypeChange(string value)
        {
            if (value == _selectedBehavior.Type) return;
            if (value == "AreaAttack")
            {
                AreaAttackBehaviorDto aab = new AreaAttackBehaviorDto(_selectedBehavior);
                GameDataDto.Database.Behaviors[aab.Code] = aab;
                _selectedBehavior = aab;
            }
            else if (value == "SelfEffect")
            {
                SelfEffectBehaviorDto seb = new SelfEffectBehaviorDto(_selectedBehavior);
                GameDataDto.Database.Behaviors[seb.Code] = seb;
                _selectedBehavior = seb;
            }
            StateHasChanged();
        }
        
        private string AreaAttack_GetApplyingBuffName(int code)
        {
            if (code < 0) return "None";
            return GameDataDto.Database.BuffSets[code].Name;
        }

    }
}
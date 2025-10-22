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
            "AreaInvocation", "SelfInvocation"
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
        private async Task AreaInvocation_PickBuffSet()
        {
            var parameters = new DialogParameters<BuffSetPickerDialog>
            {
                { x => x.AllBuffSets, GameDataDto.Database.BuffSets}, // Database의 모든 BuffSet 전달.
            };
            var dialog = await DialogService.ShowAsync<BuffSetPickerDialog>("Select Buffset", parameters);
            var result = await dialog.Result;
            if (result != null && !result.Canceled && _selectedBehavior is AreaInvocationBehaviorDto)
            {
                (_selectedBehavior as AreaInvocationBehaviorDto).ApplyingBuffSetCode = ((BuffSetDto)result.Data).Code;
                StateHasChanged();
            }
        }
        private async Task OpenScopePicker()
        {
            var parameters = new DialogParameters<EnumPickerDialog<BasicPatternType>>
            {
                { x => x.Title, "Select Scope" },
                { x => x.AllItems, Enum.GetValues<BasicPatternType>() }, // 모든 Scope 목록 전달
                { x => x.IsMultiSelect, false }
            };

            var dialog = await DialogService.ShowAsync<EnumPickerDialog<BasicPatternType>>("Select Scope", parameters);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                var selectedSet = (HashSet<BasicPatternType>)result.Data;
                _selectedBehavior.Scope = selectedSet.FirstOrDefault();
            }
        }
        private async Task OpenTagPicker()
        {

            var parameters = new DialogParameters<EnumPickerDialog<BehaviorTag>>
            {
                { x => x.Title, "Select a Tag to Add" },
                { x => x.AllItems, Enum.GetValues<BehaviorTag>() },
                { x => x.IsMultiSelect, false }
            };

            var dialog = await DialogService.ShowAsync<EnumPickerDialog<BehaviorTag>>("Select a Tag to Add", parameters);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                var selectedSet = (HashSet<BehaviorTag>)result.Data;
                _selectedBehavior.Tags.Add(selectedSet.FirstOrDefault());
            }
        }
        private async Task OpenTargetPicker()
        {

            var parameters = new DialogParameters<EnumPickerDialog<TeamType>>
            {
                { x => x.Title, "Select a Type to Add" },
                { x => x.AllItems, Enum.GetValues<TeamType>() },
                { x => x.IsMultiSelect, false }
            };

            var dialog = await DialogService.ShowAsync<EnumPickerDialog<TeamType>>("Select a Type to Add", parameters);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                var selectedSet = (HashSet<TeamType>)result.Data;
                _selectedBehavior.Accessible.Add(selectedSet.FirstOrDefault());
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

                // 새로운 최대치가 현재 개수보다 많으면, 빈 항목을 추가합니다.(가장 단순한 SelfInvocationBehavior로.)
                if (newMax > currentCount)
                {
                    for (int i = currentCount; i < newMax; i++)
                    {
                        _behaviors.Add(new SelfInvocationBehaviorDto { Code = i, Name = "" });
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
            if (value == "AreaInvocation")
            {
                AreaInvocationBehaviorDto aib = new AreaInvocationBehaviorDto(_selectedBehavior);
                GameDataDto.Database.Behaviors[aib.Code] = aib;
                _selectedBehavior = aib;
            }
            else if (value == "SelfInvocation")
            {
                SelfInvocationBehaviorDto seb = new SelfInvocationBehaviorDto(_selectedBehavior);
                GameDataDto.Database.Behaviors[seb.Code] = seb;
                _selectedBehavior = seb;
            }
            StateHasChanged();
        }

        private void RemoveBehaviorTag(BehaviorTag tagToRemove)
        {
            if (_selectedBehavior?.Tags != null)
            {
                _selectedBehavior.Tags.Remove(tagToRemove);
            }
        }
        private void RemoveValidTarget(TeamType TargetToRemove)
        {
            if (_selectedBehavior?.Tags != null)
            {
                _selectedBehavior.Accessible.Remove(TargetToRemove);
            }
        }
        
        
        private string AreaInvocation_GetApplyingBuffName(int code)
        {
            if (code < 0) return "None";
            return GameDataDto.Database.BuffSets[code].Name;
        }

    }
}
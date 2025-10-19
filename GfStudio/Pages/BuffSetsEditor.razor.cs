using GfToolkit.Shared.Dtos.Models;
using GfToolkit.Shared.Dtos;
using GfToolkit.Shared;
using GfStudio.Dialogs;
using MudBlazor;
using System.Reflection.Metadata;
namespace GfStudio.Pages
{
    public partial class BuffSetEditor
    {
        private List<BuffSetDto> _buffSets = GameDataDto.Database.BuffSets;
        private BuffSetDto _selectedBuffSet { get; set; }
        protected override void OnInitialized()
        {
            _selectedBuffSet = _buffSets.FirstOrDefault();
        }
        private void HandleBuffSetSelected(BuffSetDto selected)
        {
            _selectedBuffSet = selected;
        }
        private async Task OpenAddEffectDialog()
        {
            var parameters = new DialogParameters<EffectEditorDialog>();
            parameters.Add(x => x.AllBuffSets, _buffSets);
            var dialogReference = await DialogService.ShowAsync<EffectEditorDialog>("Add New Effect", parameters);
            var result = await dialogReference.Result;
            if (result != null && !result.Canceled)
            {
                var newEffect = result.Data as BuffDto;
                _selectedBuffSet.Effects.Add(newEffect);
                StateHasChanged();
            }
        }
        private async Task OnDeleteEffectClicked(BuffDto effectToDelete)
        {
            // 1. 확인창을 띄웁니다.
            bool? result = await DialogService.ShowMessageBox(
                "Warning",
                "Deleting can not be undone!",
                yesText: "Delete!", cancelText: "Cancel");
            // 2. 사용자가 '삭제' 버튼을 눌렀을 때만 (취소하지 않았을 때)
            if (result != null)
            {
                // 3. 리스트에서 해당 효과를 제거합니다.
                _selectedBuffSet.Effects.Remove(effectToDelete);
                // 4. 화면을 새로고침합니다.
                StateHasChanged();
            }
        }
        private async Task OnEditEffectClicked(BuffDto effectToEdit)
        {
            // 1. 팝업에 전달할 파라미터를 만듭니다.
            var parameters = new DialogParameters<EffectEditorDialog>
        {
            { x => x.AllBuffSets, _buffSets }, // 전체 버프셋 목록 전달
            { x => x.EffectToEdit, effectToEdit } // "이 데이터로 수정해줘!" 라고 전달
        };

            // 2. 팝업을 띄우고 결과를 기다립니다.
            var dialog = await DialogService.ShowAsync<EffectEditorDialog>("Edit Effect", parameters);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                var editedEffect = (BuffDto)result.Data;

                // 3. 기존 데이터를 찾아서 수정된 데이터로 교체합니다.
                var index = _selectedBuffSet.Effects.IndexOf(effectToEdit);
                if (index != -1)
                {
                    _selectedBuffSet.Effects[index] = editedEffect;
                }

                StateHasChanged();
            }
        }
        private async Task OpenChangeMaxDialog()
        {
            var parameters = new DialogParameters<ChangeMaxDialog>
            {
                { x => x._oldMaximum, _buffSets.Count }, // 기존 maximum 전달.
            };
            var dialog = await DialogService.ShowAsync<ChangeMaxDialog>("Change Maximum Value", parameters);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                var newMax = (int)result.Data;
                var currentCount = _buffSets.Count;

                // 새로운 최대치가 현재 개수보다 많으면, 빈 항목을 추가합니다.
                if (newMax > currentCount)
                {
                    for (int i = currentCount; i < newMax; i++)
                    {
                        _buffSets.Add(new BuffSetDto { Code = i, Name = "" });
                    }
                }
                // 새로운 최대치가 현재 개수보다 적으면, 뒤에서부터 항목을 삭제합니다.
                else if (newMax < currentCount)
                {
                    _buffSets.RemoveRange(newMax, currentCount - newMax);
                }
                
                StateHasChanged(); // 리스트가 변경되었으니 화면을 새로고침합니다.
            }
        }
    }
}
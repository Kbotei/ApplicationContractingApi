namespace ApplicationContractingApi.Models.Network;

public record OrderedField : SimpleField
{
    public string? LabelText { get; set; }

    public string? SelectedItemText { get; set; }

    public int ViewOrder { get; set; }
}

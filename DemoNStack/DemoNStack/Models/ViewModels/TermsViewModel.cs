namespace DemoNStack.Models.ViewModels;

public class TermsViewModel
{
    public string Content { get; set; } = string.Empty;

    public bool HasApproved { get; set; }

    public string HasApprovedString { get; set; } = string.Empty;

    public string ApproveButton { get; set; } = string.Empty;

    public string Headline { get; set; } = string.Empty;
}

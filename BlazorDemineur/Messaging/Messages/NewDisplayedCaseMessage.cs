using BlazorDemineur.Pages;

namespace BlazorDemineur.Messaging.Messages
{
    public class NewDisplayedCaseMessage(Case displayedCase)
    {
        public Case DisplayedCase { get; init; } = displayedCase;
    }
}

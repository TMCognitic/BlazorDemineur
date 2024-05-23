using BlazorDemineur.Pages;

namespace BlazorDemineur.Messaging.Messages
{
    public class NewBombMessage(Case bomb)
    {
        public Case Bomb { get; init; } = bomb;
    }
}

using BlazorDemineur.Messaging;
using BlazorDemineur.Messaging.Messages;
using BlazorDemineur.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.Design;

namespace BlazorDemineur.Pages
{
    public partial class Case : IDisposable
    {
        private static bool FirstClick = true;
        private bool _display;

        [Inject]
        public IMediator<NewBombMessage>? NewBombMessageHandler { get; set; }

        [Inject]
        public IMediator<NewDisplayedCaseMessage>? DisplayedCaseMessageHandler { get; set; }

        [Parameter]
        public EventCallback<Case> CaseExplosion { get; set; }

        [Parameter]
        public EventCallback<Case> OnFirstClick { get; set; }

        [Parameter]
        public EventCallback<Case> OnCreated { get; set; }

        [Parameter]
        public int X { get; set; }
        [Parameter]
        public int Y { get; set; }

        public char Content
        {
            get
            {
                return Value switch
                {
                    CaseValue.Empty => ' ',
                    CaseValue.One => '1',
                    CaseValue.Two => '2',
                    CaseValue.Three => '3',
                    CaseValue.Four => '4',
                    CaseValue.Five => '5',
                    CaseValue.Six => '6',
                    CaseValue.Seven => '7',
                    CaseValue.Eight => '8',
                    _ => 'X',
                };
            }             
        }

        public string Style
        {
            get { return $"p{(int)Value}"; }
        }

        public CaseValue Value { get; private set; }

        public bool Display 
        {
            get { return _display; }
            set 
            {
                if (_display != value)
                {
                    _display = value;

                    if (Value is CaseValue.Empty)
                    {
                        DisplayedCaseMessageHandler?.Send(new NewDisplayedCaseMessage(this));
                    }
                    StateHasChanged();
                }
            }
        }

        protected override void OnInitialized()
        {
            NewBombMessageHandler?.Register(OnNewBombMessage);
            DisplayedCaseMessageHandler?.Register(OnNewDisplayedCase);
        }

        private void OnNewDisplayedCase(NewDisplayedCaseMessage message)
        {
            Case displayedCase = message.DisplayedCase;

            if (Math.Abs(X - displayedCase.X) > 1)
                return;

            if (Math.Abs(Y - displayedCase.Y) > 1)
                return;

            if (Display)
                return;

            Display = true;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                OnCreated.InvokeAsync(this);
            }
        }

        private void OnNewBombMessage(NewBombMessage message)
        {
            Case bomb = message.Bomb;

            if(bomb == this)
            {
                return;
            }

            if(Value is CaseValue.Bomb)
            {
                return;
            }

            if (Math.Abs(X - bomb.X) < 2 && Math.Abs(Y - bomb.Y) < 2)
            {
                Value++;
            }
        }

        private void OnMouseDown(MouseEventArgs e)
        {
            if (Display)
            {
                return;
            }

            if (FirstClick)
            {
                FirstClick = false;
                OnFirstClick.InvokeAsync(this);
            }

            switch (e.Button)
            {
                case 0:
                    //Left Button
                    Display = true;
                    if(Value is CaseValue.Bomb)
                        CaseExplosion.InvokeAsync(this);
                    break;
                case 2:
                    //Right Button
                    break;
            }
        }

        public void SetAsBomb()
        {
            Value = CaseValue.Bomb;
            NewBombMessageHandler?.Send(new NewBombMessage(this));
        }

        public void Dispose()
        {
            NewBombMessageHandler?.Unregister(OnNewBombMessage);
            GC.SuppressFinalize(this);
        }
    }
}

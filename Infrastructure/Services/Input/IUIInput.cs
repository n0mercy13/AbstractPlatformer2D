using System;

namespace Codebase.Infrastructure
{
    public interface IUIInput
    {
        event Action OpenSettingsPressed;
        event Action CanceledPressed;
        event Action SubmitPressed;
    }
}
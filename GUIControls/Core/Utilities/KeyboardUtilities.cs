using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GUIControls.Core.Utilities
{
  internal class KeyboardUtilities
  {
    internal static bool IsKeyModifyingPopupState( KeyEventArgs e )
    {
      return ( ( ( ( e.SystemKey == Key.Down ) || ( e.SystemKey == Key.Up ) ) )
            || ( e.Key == Key.F4 ) );
    }
  }
}

/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus edition at http://xceed.com/wpf_toolkit

   Visit http://xceed.com and follow @datagrid on Twitter

  **********************************************************************/

using System;

namespace GUIControls
{
  [Flags]
  public enum AllowedSpecialValues
  {
    None = 0,
    NaN = 1,
    PositiveInfinity = 2,
    NegativeInfinity = 4,
    AnyInfinity = PositiveInfinity | NegativeInfinity,
    Any = NaN | AnyInfinity
  }
}

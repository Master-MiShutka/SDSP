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
using System.Windows;

namespace GUIControls
{
  public class ShortUpDown : CommonNumericUpDown<short>
  {
    #region Constructors

    static ShortUpDown()
    {
      UpdateMetadata( typeof( ShortUpDown ), ( short )1, short.MinValue, short.MaxValue );
    }

    public ShortUpDown()
      : base( Int16.Parse, Decimal.ToInt16, ( v1, v2 ) => v1 < v2, ( v1, v2 ) => v1 > v2 )
    {
    }

    #endregion //Constructors

    #region Base Class Overrides

    protected override short IncrementValue( short value, short increment )
    {
      return ( short )( value + increment );
    }

    protected override short DecrementValue( short value, short increment )
    {
      return ( short )( value - increment );
    }

    #endregion //Base Class Overrides
  }
}

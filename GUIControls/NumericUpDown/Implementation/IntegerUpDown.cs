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
  public class IntegerUpDown : CommonNumericUpDown<int>
  {
    #region Constructors

    static IntegerUpDown()
    {
      UpdateMetadata( typeof( IntegerUpDown ), 1, int.MinValue, int.MaxValue );
    }

    public IntegerUpDown()
      : base( Int32.Parse, Decimal.ToInt32, ( v1, v2 ) => v1 < v2, ( v1, v2 ) => v1 > v2 )
    {
    }

    #endregion //Constructors

    #region Base Class Overrides

    protected override int IncrementValue( int value, int increment )
    {
      return value + increment;
    }

    protected override int DecrementValue( int value, int increment )
    {
      return value - increment;
    }

    #endregion //Base Class Overrides
  }
}

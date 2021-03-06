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
  internal class ULongUpDown : CommonNumericUpDown<ulong>
  {
    #region Constructors

    static ULongUpDown()
    {
      UpdateMetadata( typeof( ULongUpDown ), ( ulong )1, ulong.MinValue, ulong.MaxValue );
    }

    public ULongUpDown()
      : base( ulong.Parse, Decimal.ToUInt64, ( v1, v2 ) => v1 < v2, ( v1, v2 ) => v1 > v2 )
    {
    }

    #endregion //Constructors

    #region Base Class Overrides

    protected override ulong IncrementValue( ulong value, ulong increment )
    {
      return ( ulong )( value + increment );
    }

    protected override ulong DecrementValue( ulong value, ulong increment )
    {
      return ( ulong )( value - increment );
    }

    #endregion //Base Class Overrides
  }
}

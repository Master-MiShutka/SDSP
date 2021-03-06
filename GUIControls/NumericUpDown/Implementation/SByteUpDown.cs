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
  internal class SByteUpDown : CommonNumericUpDown<sbyte>
  {
    #region Constructors

    static SByteUpDown()
    {
      UpdateMetadata( typeof( SByteUpDown ), ( sbyte )1, sbyte.MinValue, sbyte.MaxValue );
    }

    public SByteUpDown()
      : base( sbyte.Parse, Decimal.ToSByte, ( v1, v2 ) => v1 < v2, ( v1, v2 ) => v1 > v2 )
    {
    }

    #endregion //Constructors

    #region Base Class Overrides

    protected override sbyte IncrementValue( sbyte value, sbyte increment )
    {
      return ( sbyte )( value + increment );
    }

    protected override sbyte DecrementValue( sbyte value, sbyte increment )
    {
      return ( sbyte )( value - increment );
    }

    #endregion //Base Class Overrides
  }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.Controls;

namespace Rop.Winforms9.ColumnsListBox
{
    public partial class ColumnListBox
    {
       protected ColumnPanel Header { get;}
       [DefaultValue(null)]
       public IBankIcon? BankIcon
       {
           get => Header.BankIcon;
           set => Header.BankIcon = value;
       }
       [DefaultValue(false)]
       public bool HeaderBorderRaised
        {
            get => Header.BorderRaised;
            set => Header.BorderRaised = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BorderStyle HeaderBorderStyle
        {
            get => Header.BorderStyle;
            set => Header.BorderStyle = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<ColumnDefinition> ColumnDefinitions
        {
            get => Header.ColumnDefinitions;
            set => Header.ColumnDefinitions = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color HeaderBackColor
        {
            get => Header.BackColor;
            set => Header.BackColor = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Padding ColumnsPadding
        {
            get => Header.ColumnsPadding;
            set => Header.ColumnsPadding = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool HeaderInteriorBorder
        {
            get => Header.InteriorBorder;
            set => Header.InteriorBorder = value;
        }
        [DefaultValue(typeof(Cursor),"Hand")]
        public Cursor HeaderSelectableCursor
        {
            get => Header.SelectableCursor;
            set => Header.SelectableCursor = value;
        }
    }
}
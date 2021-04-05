using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1.Enums;

namespace WpfApp1.Extensions
{
    public static class FieldTypeExtension
    {
        public static bool IsChip(this FieldType fieldType)
        {
            return new List<FieldType>
            {
                FieldType.Black,
                FieldType.White,
                FieldType.King,
            }.Contains(fieldType);
        }

        public static bool IsWhite(this FieldType fieldType)
        {
            return new List<FieldType>
            {
                FieldType.White,
                FieldType.King,
            }.Contains(fieldType);
        }

        public static bool IsBlack(this FieldType fieldType)
        {
            return new List<FieldType>
            {
                FieldType.Black,
            }.Contains(fieldType);
        }

        public static bool IsEnemy(this FieldType fieldType, FieldType field)
        {
            // TODO ???
            if (fieldType == FieldType.King)
                return false;

            if (!fieldType.IsChip() || field == FieldType.Empty)
                return false;
            if (field == FieldType.Throne ||
                field == FieldType.Exit)
                return true;
            return fieldType.IsBlack() != field.IsWhite();
        }
    }
}

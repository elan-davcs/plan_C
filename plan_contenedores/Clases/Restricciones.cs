using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan_contenedores.Clases
{
    internal class Restricciones
    {
        public static void Restriccion_contenedor(Entry entry)
        {
            entry.TextChanged += (sender, e) =>
            {
                if (e.NewTextValue.Length <= 4)
                {
                    // Asegurar que los primeros 4 caracteres sean letras y en mayúsculas
                    entry.Text = new string(e.NewTextValue.Take(4).Where(char.IsLetter).Select(char.ToUpper).ToArray());
                }
                else
                {
                    // Permitir solo números después de los primeros 4 caracteres
                    entry.Text = e.NewTextValue.Substring(0, 4) + new string(e.NewTextValue.Skip(4).Where(char.IsDigit).ToArray());
                }
            };
        }
        public static void Restriccion_letras_numeros(Entry entry)
        {
            entry.TextChanged += (sender, e) =>
            {
                // Permitir solo letras y números
                string newText = new string(e.NewTextValue.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());

                // Convertir letras a mayúsculas
                newText = newText.ToUpper();

                entry.Text = newText;
            };
        }
        public static void Restriccion_letras(Entry entry)
        {
            entry.TextChanged += (sender, e) =>
            {
                // Permitir solo letras y espacios
                string newText = new string(e.NewTextValue.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());

                // Convertir letras a mayúsculas
                newText = newText.ToUpper();

                entry.Text = newText;
            };
        }
        public static void Restriccion_numeros(Entry entry)
        {
            entry.TextChanged += (sender, e) =>
            {
                // Permitir solo letras y espacios
                string newText = new string(e.NewTextValue.Where(c => char.IsDigit(c) || char.IsWhiteSpace(c)).ToArray());

                // Convertir letras a mayúsculas
                newText = newText.ToUpper();

                entry.Text = newText;
            };
        }
        public static void Restriccion_sellos(Entry entry)
        {
            entry.TextChanged += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.NewTextValue))
                {
                    // Filtrar solo números y letras
                    string cleanedText = new string(e.NewTextValue.Where(c => char.IsLetterOrDigit(c)).ToArray());

                    entry.Text = cleanedText;
                }
            };
        }
        public static void Formato_isotype(Entry entry)
        {
            entry.TextChanged += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.NewTextValue))
                {
                    // Remover guiones existentes
                    string cleanText = e.NewTextValue.Replace("-", "");

                    // Limitar a 4 caracteres
                    cleanText = cleanText.Length <= 2 ? cleanText : cleanText.Substring(0, 2);

                    // Agregar guiones
                    string formattedText = string.Join("-", cleanText.ToCharArray());

                    entry.Text = formattedText;
                }
            };
        }
    }
}

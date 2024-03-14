using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System;

namespace Domain.Entities
{
    public class NomePessoaJuridica : INomePessoa
    {
        readonly List<string> preposicoes = new List<string> { "de", "do", "da", "dos", "das", "e" };

        protected NomePessoaJuridica()
        { }

        public NomePessoaJuridica(string razaoSocial)
        {
            this.NomeCompleto = razaoSocial;
        }

        public virtual string NomeCompleto { get; set; }

        private string _nomeAbreviado;
        public virtual string NomeAbreviado
        {
            get
            {
                int nameMaxLength = 35;

                if (!string.IsNullOrEmpty(_nomeAbreviado))
                {
                    return _nomeAbreviado.Substring(0, Math.Min(_nomeAbreviado.Length, nameMaxLength));
                }
                else if (this.NomeCompleto.Length <= nameMaxLength)
                {
                    return this.NomeCompleto;
                }
                else
                {
                    // Quebro os nomes...
                    var nomes = this.NomeCompleto.Split(' ').ToList();

                    int i = 1;
                    while (string.Join(" ", nomes).Length > nameMaxLength && (i < nomes.Count - 1))
                    {
                        if (!preposicoes.Contains(nomes[i].ToLower()) && nomes[i].Trim().Length > 0)
                            nomes[i] = nomes[i].Substring(0, 1);

                        i++;
                    }

                    var nmAbreviado = string.Join(" ", nomes.Where(x => x.Trim().Length > 0));

                    return nmAbreviado.Substring(0, Math.Min(nmAbreviado.Length, nameMaxLength));
                }

            }
            set
            {
                _nomeAbreviado = value;
            }
        }

        public override string ToString()
        {
            return this.NomeCompleto;
        }

        public bool StartsWith(string value)
        {
            return this.NomeCompleto.StartsWith(value);
        }

        public bool Contains(string value)
        {
            return this.NomeCompleto.Contains(value);
        }

        public int CompareTo(object obj)
        {
            return this.NomeCompleto.CompareTo(obj);
        }

        public int CompareTo(string other)
        {
            return this.NomeCompleto.CompareTo(other);
        }

        public bool Equals(string other)
        {
            return this.NomeCompleto.Equals(other);
        }

        public string ToUpper()
        {
            return this.ToString().ToUpper();
        }

        public string ToUpper(CultureInfo culture)
        {
            return this.ToString().ToUpper(culture);
        }

        public string ToUpperInvariant()
        {
            return this.ToString().ToUpperInvariant();
        }

    }
}
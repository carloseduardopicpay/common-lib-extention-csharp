using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Domain.Entities
{
    public class NomePessoaFisica : INomePessoa
    {
        readonly List<string> preposicoes = new List<string> { "de", "do", "da", "dos", "das", "e" };

        protected NomePessoaFisica()
        { }

        public NomePessoaFisica(string nomeCompleto)
        {
            if (nomeCompleto.Trim().Length == 0)
            {
                Primeiro = string.Empty;
                SobreNome = string.Empty;
                return;
            }

            Primeiro = nomeCompleto;
       
        }

        public NomePessoaFisica(string primeiroNome, string sobrennome)
        {
            this.Primeiro = primeiroNome;
            this.SobreNome = sobrennome;
        }

        public NomePessoaFisica(string primeiroNome, string nomeMeio, string sobrennome)
        {
            this.Primeiro = primeiroNome;
            this.Meio = nomeMeio;
            this.SobreNome = sobrennome;
        }

        public NomePessoaFisica(Dominio prefixo, string primeiroNome, string nomeMeio, string sobrennome, Dominio sufixo)
        {
            this.Prefixo = prefixo;
            this.Primeiro = primeiroNome;
            this.Meio = nomeMeio;
            this.SobreNome = sobrennome;
            this.Sufixo = sufixo;
        }

        public virtual Dominio Prefixo { get; set; }

        private string _primeiroNome;
        public virtual string Primeiro
        {
            get
            {
                return _primeiroNome;
            }
            set
            {
                _primeiroNome = string.IsNullOrEmpty(value) ? value : value.ToUpper().Trim();
            }
        }

        private string _nomeMeio;
        public virtual string Meio
        {
            get
            {
                return _nomeMeio;
            }
            set
            {
                _nomeMeio = string.IsNullOrEmpty(value) ? value : value.ToUpper().Trim();
            }
        }

        private string _sobrenome;
        public virtual string SobreNome
        {
            get
            {
                return _sobrenome;
            }
            set
            {
                _sobrenome = string.IsNullOrEmpty(value) ? value : value.ToUpper().Trim();
            }
        }

        public virtual Dominio Sufixo { get; set; }

        public override string ToString()
        {
            string nome = this.NomeCompleto;

            return nome.ToUpper();
        }

        private string _nome;
        public virtual string NomeCompleto
        {
            get
            {
                string nome = string.IsNullOrEmpty(Primeiro) ? string.Empty : Primeiro.Trim();
                nome = string.Concat(nome, " ", Meio).Trim();
                nome = string.Concat(nome, " ", SobreNome).Trim();
                nome = string.Concat(nome, " ", Sufixo).Trim();

                if (!string.IsNullOrEmpty(_nome) && nome.Equals(_nome))
                    return _nome.ToUpper();
                else
                {
                    return nome.ToUpper();
                }
            }
            protected set
            {
                _nome = value;
            }
        }

        private string _nomeAbreviado;
        public virtual string NomeAbreviado
        {
            get
            {
                int nameMaxLength = 35;

                if (this.NomeCompleto.Length <= nameMaxLength)
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

                    return string.Join(" ", nomes.Where(x => x.Trim().Length > 0));
                }

            }
            set
            {
                _nomeAbreviado = value;
            }
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
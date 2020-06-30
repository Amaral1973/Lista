using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLIsta
{
    class Pessoa
    {
        //Definir uma classe pública Pessoa
        public Pessoa()
        {

        }

        //Construção da classe Pessoa com o construtor e a definição dos tipos
        public Pessoa(int id, string nome, short idade, char sexo)
        {
            this.p_id = id;
            this.p_nome = nome;
            this.p_idade = idade;
            this.p_sexo = sexo;
        }

        //Criar as variáveis e seus valores padrões dentro da classe Pessoa - Lista Pessoa
        private int p_id = -1;
        private string p_nome = String.Empty;
        private short p_idade = 0;
        private char? p_sexo = null;

        //Construtores de cada componente da classe Pessoa
        public int ID
        {
            get { return p_id; }
            set { p_id = value; }
        }

        public string Nome
        {
            get { return p_nome; }
            set { p_nome = value; }
        }

        public short Idade
        {
            get { return p_idade; }
            set { p_idade = value; }
        }

        public char? Sexo        {
            get { return p_sexo; }
            set { p_sexo = value; }
        }
    }
}

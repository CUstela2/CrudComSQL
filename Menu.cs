
namespace SQL_com_C_
{
    public class Menu
    {
        private FilmeRepositorio filmeRepo;
        private SessaoRepositorio sessaoRepo;

        public Menu(string connectionString)
        {
            filmeRepo = new FilmeRepositorio(connectionString);
            sessaoRepo = new SessaoRepositorio(connectionString);
        }

        public void Iniciar()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Gerenciamento de Cinema ===");
                Console.WriteLine("1 - Cadastrar Filme");
                Console.WriteLine("2 - Listar Filmes");
                Console.WriteLine("3 - Atualizar Filme");
                Console.WriteLine("4 - Excluir Filme");
                Console.WriteLine("5 - Cadastrar Sessão");
                Console.WriteLine("6 - Listar Sessões de um Filme");
                Console.WriteLine("7 - Atualizar Sessão");
                Console.WriteLine("8 - Excluir Sessão");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": CadastrarFilme(); break;
                    case "2": ListarFilmes(); break;
                    case "3": AtualizarFilme(); break;
                    case "4": ExcluirFilme(); break;
                    case "5": CadastrarSessao(); break;
                    case "6": ListarSessoesFilme(); break;
                    case "7": AtualizarSessao(); break;
                    case "8": ExcluirSessao(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
                Console.WriteLine("Pressione ENTER para continuar...");
                Console.ReadLine();
            }
        }

        private void CadastrarFilme()
        {
            Console.WriteLine("== Cadastrar Filme ==");
            Console.Write("Título: ");
            var titulo = Console.ReadLine();
            Console.Write("Gênero: ");
            var genero = Console.ReadLine();
            Console.Write("Ano: ");
            int.TryParse(Console.ReadLine(), out int ano);

            var filme = new Filme { Titulo = titulo, Genero = genero, Ano = ano };
            filmeRepo.Cadastrar(filme);
            Console.WriteLine("Filme cadastrado com sucesso!");
        }

        private void ListarFilmes()
        {
            Console.WriteLine("== Lista de Filmes ==");
            var filmes = filmeRepo.Listar();
            foreach (var f in filmes)
            {
                Console.WriteLine($"{f.IdFilme} - {f.Titulo} ({f.Genero}, {f.Ano})");
            }
        }

        private void AtualizarFilme()
        {
            ListarFilmes();
            Console.Write("Digite o ID do filme para atualizar: ");
            int.TryParse(Console.ReadLine(), out int id);
            var filmes = filmeRepo.Listar();
            var filme = filmes.Find(f => f.IdFilme == id);

            if (filme == null)
            {
                Console.WriteLine("Filme não encontrado.");
                return;
            }

            Console.Write("Novo título (vazio para manter): ");
            var titulo = Console.ReadLine();
            if (!string.IsNullOrEmpty(titulo)) filme.Titulo = titulo;

            Console.Write("Novo gênero (vazio para manter): ");
            var genero = Console.ReadLine();
            if (!string.IsNullOrEmpty(genero)) filme.Genero = genero;

            Console.Write("Novo ano (vazio para manter): ");
            var anoStr = Console.ReadLine();
            if (int.TryParse(anoStr, out int ano)) filme.Ano = ano;

            filmeRepo.Atualizar(filme);
            Console.WriteLine("Filme atualizado!");
        }

        private void ExcluirFilme()
        {
            ListarFilmes();
            Console.Write("Digite o ID do filme para excluir: ");
            int.TryParse(Console.ReadLine(), out int id);
            filmeRepo.Excluir(id);
            Console.WriteLine("Filme e suas sessões excluídos.");
        }

        private void CadastrarSessao()
        {
            ListarFilmes();
            Console.Write("Digite o ID do filme para cadastrar sessão: ");
            int.TryParse(Console.ReadLine(), out int idFilme);

            Console.Write("Data (yyyy-mm-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime data);

            Console.Write("Hora (HH:mm): ");
            TimeSpan.TryParse(Console.ReadLine(), out TimeSpan hora);

            var sessao = new Sessao { IdFilme = idFilme, Data = data, Hora = hora };
            sessaoRepo.Cadastrar(sessao);
            Console.WriteLine("Sessão cadastrada!");
        }

        private void ListarSessoesFilme()
        {
            ListarFilmes();
            Console.Write("Digite o ID do filme para listar sessões: ");
            int.TryParse(Console.ReadLine(), out int idFilme);

            var sessoes = sessaoRepo.ListarPorFilme(idFilme);
            Console.WriteLine($"Sessões do filme {idFilme}:");
            foreach (var s in sessoes)
            {
                Console.WriteLine($"{s.IdSessao} - {s.Data.ToShortDateString()} às {s.Hora}");
            }
        }

        private void AtualizarSessao()
        {
            ListarFilmes();
            Console.Write("Digite o ID do filme: ");
            int.TryParse(Console.ReadLine(), out int idFilme);

            var sessoes = sessaoRepo.ListarPorFilme(idFilme);
            foreach (var s in sessoes)
            {
                Console.WriteLine($"{s.IdSessao} - {s.Data.ToShortDateString()} às {s.Hora}");
            }

            Console.Write("Digite o ID da sessão para atualizar: ");
            int.TryParse(Console.ReadLine(), out int idSessao);

            var sessao = sessoes.Find(s => s.IdSessao == idSessao);
            if (sessao == null)
            {
                Console.WriteLine("Sessão não encontrada.");
                return;
            }

            Console.Write("Nova data (yyyy-mm-dd, vazio para manter): ");
            var dataStr = Console.ReadLine();
            if (DateTime.TryParse(dataStr, out DateTime data)) sessao.Data = data;

            Console.Write("Nova hora (HH:mm, vazio para manter): ");
            var horaStr = Console.ReadLine();
            if (TimeSpan.TryParse(horaStr, out TimeSpan hora)) sessao.Hora = hora;

            sessaoRepo.Atualizar(sessao);
            Console.WriteLine("Sessão atualizada!");
        }

        private void ExcluirSessao()
        {
            ListarFilmes();
            Console.Write("Digite o ID do filme: ");
            int.TryParse(Console.ReadLine(), out int idFilme);

            var sessoes = sessaoRepo.ListarPorFilme(idFilme);
            foreach (var s in sessoes)
            {
                Console.WriteLine($"{s.IdSessao} - {s.Data.ToShortDateString()} às {s.Hora}");
            }

            Console.Write("Digite o ID da sessão para excluir: ");
            int.TryParse(Console.ReadLine(), out int idSessao);

            sessaoRepo.Excluir(idSessao);
            Console.WriteLine("Sessão excluída.");
        }
    }
}

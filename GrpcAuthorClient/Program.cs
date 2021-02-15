using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using System.Runtime.InteropServices;

namespace GrpcAuthorClient
{
    class Program {
        //static async Task Main(string[] args) {
        //    var serverAddress = "https://localhost:5001";
        //    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
        //        // The following statement allows you to call insecure services. To be used only in development environments.
        //        AppContext.SetSwitch(
        //            "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        //        serverAddress = "http://localhost:5000";
        //    }

        //    using var channel = GrpcChannel.ForAddress(serverAddress);
        //    var client = new Author.AuthorClient(channel);

        //    var reply = await client.GetAuthorAsync(new AuthorRequest { Name = "Antonio Gonzalez" });

        //    Console.WriteLine("Author: " + reply.ToString());

        //    Console.WriteLine("Press any key to exit...");
        //    Console.ReadKey();
        //}

        static async Task Main(string[] args) {
            Console.WriteLine("Command line. Write 'help' for more information.");
            TypeACommand();
            string cmd = String.Empty;

            while (true) {
                try {
                    
                    cmd = Console.ReadLine();
                    var commands = cmd.Split(' ');
                    var action = commands[0];
                    switch (action) {
                        case "help":
                            HelpCommand();
                            break;
                        case "getAuthor":
                            GetAuthorCommand(commands);
                            break;
                        case "exit":
                            return;
                        default:
                            NotRecognizedCommand(cmd);
                            break;
                    }

                }
                catch (Exception) {
                    NotRecognizedCommand(cmd);
                }
                
            }

            Console.ReadKey();
        }

        static void NotRecognizedCommand(string cmd) {
            Console.WriteLine("'" + cmd + "' is not recognized as an internal or external command, operable program or batch file.");
            TypeACommand();
        }

        static void HelpCommand() {
            Console.WriteLine("**************** Options ****************");
            Console.WriteLine("1) getAuthor nameAuthor: get all the author's books.");
            Console.WriteLine("2) exit: End up the program.");
            Console.WriteLine("3) ariel: hola que haces");
            Console.WriteLine("*****************************************");
            Console.WriteLine("");
            TypeACommand();
        }

        static async void GetAuthorCommand(string[] commands) {
            try {
                string authorName = String.Empty;
                for (int i = 1; i < commands.Length; i++) {
                    authorName += commands[i];
                    authorName += " ";
                }

                var serverAddress = "https://localhost:5001";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    // The following statement allows you to call insecure services. To be used only in development environments.
                    AppContext.SetSwitch(
                        "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                    serverAddress = "http://localhost:5000";
                }

                using var channel = GrpcChannel.ForAddress(serverAddress);
                var client = new Author.AuthorClient(channel);
                var reply = await client.GetAuthorAsync(new AuthorRequest { Name = authorName.Trim() });

                Console.WriteLine("Author: " + reply.ToString());
                Console.WriteLine("");

                TypeACommand();
            }
            catch (Exception e) {
                Console.WriteLine("Incorrect number of parameters");
            }

        }

        private static void TypeACommand() {
            Console.WriteLine("Type a command");
        }
        //Commands
        //help: information about commands
        //getAuthor authorName
        //exit: close instance
        //ariel

    }

    
}

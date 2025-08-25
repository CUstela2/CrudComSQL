using SQL_com_C_;

string connectionString = @"Server=localhost\SQLEXPRESS;Database=CRUDdb;Trusted_Connection=True;";
Menu menu = new Menu(connectionString);
menu.Iniciar();
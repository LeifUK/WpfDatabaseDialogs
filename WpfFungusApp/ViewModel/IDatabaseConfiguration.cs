namespace WpfFungusApp.ViewModel
{
    interface IDatabaseConfiguration
    {
        string SQLite_Folder { get; set; }
        string SQLite_Filename { get; set; }

        int SelectedSqlServerInstance { get; set; }
        bool SQLServer_UseWindowsAuthentication { get; set; }
        string SQLServer_UserName { get; set; }
        string SQLServer_Password { get; set; }
        string SQLServer_Folder { get; set; }
        string SQLServer_Filename { get; set; }

        string PostgreSQL_Host { get; set; }
        bool PostgreSQL_UseIPv6 { get; set; }
        ushort PostgreSQL_Port { get; set; }
        bool PostgreSQL_UseWindowsAuthentication { get; set; }
        string PostgreSQL_UserName { get; set; }
        string PostgreSQL_Password { get; set; }
        string PostgreSQL_DatabaseName { get; set; }

        string MySQL_Host { get; set; }
        bool MySQL_UseIPv6 { get; set; }
        ushort MySQL_Port { get; set; }
        bool MySQL_UseWindowsAuthentication { get; set; }
        string MySQL_UserName { get; set; }
        string MySQL_Password { get; set; }
        string MySQL_DatabaseName { get; set; }
    }
}

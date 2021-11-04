using RMS.Domain.Model;
using System;
using System.IO;
using RMS.Core.utilities;

namespace RMS.Core
{
    public class TeachersRecord
    {
        private string _fileName;
        public Teacher[] Teachers_Record;
        private int _maxRecordsCount;

        public TeachersRecord(string fileName)
        {
            _fileName = fileName;
            Teachers_Record = new Teacher[100];
            _maxRecordsCount = 0;
        }

        public void BeginApp()
        {
            while (true)
            {
                int selection = DisplayMainMenu();
                switch (selection)
                {
                    case 1:
                        AddRecords();
                        break;
                    case 2:
                        EditRecords();
                        break;
                    case 3:
                    {
                        ViewRecords(ConsoleColor.DarkGray,ConsoleColor.White);
                        Console.Write("Press any key to return to Main Menu: ");
                       Console.ReadLine();
                    }
                        break;
                    case 4:
                        DeleteRecords();
                        break;
                    case 5:
                    default:
                        return;
                };
            }
        }
        public int DisplayMainMenu()
        {
            CursorHelper.clrscr();
            CursorHelper.FormatScreen(ConsoleColor.DarkGreen,ConsoleColor.White);
            CursorHelper.gotoxy(10, 4);
            Console.Write("Welcome to Teachers Record Management System");
            CursorHelper.gotoxy(10, 5);
            Console.Write("___________________________________________");
            CursorHelper.gotoxy(10, 6);
            Console.Write("1. Add Teacher Record");
            CursorHelper.gotoxy(10, 7);
            Console.Write("2. Edit Teacher Record");
            CursorHelper.gotoxy(10, 8);
            Console.Write("3. View Teachers Record");
            CursorHelper.gotoxy(10, 9);
            Console.Write("4. Delete Teacher Record");
            CursorHelper.gotoxy(10, 10);
            Console.Write("5. Exit");
            CursorHelper.gotoxy(10, 11);
            Console.Write("___________________________________________");
            CursorHelper.gotoxy(10, 13);
            Console.Write("Enter your Selection: ");
            int m = -1;
            m = Convert.ToInt32(Console.ReadLine());
            return m;
        }

        private void ViewRecords(ConsoleColor bgColor,ConsoleColor fgColor)
        {
            int count = ReadTeacherDbFile();
            CursorHelper.clrscr();
            CursorHelper.FormatScreen(bgColor, fgColor);
            CursorHelper.gotoxy(10, 4);
            Console.Write("Welcome to Teachers Record Management System");
            CursorHelper.gotoxy(10, 5);
            Console.Write("___________________________________________");
            CursorHelper.gotoxy(10, 6);
            Console.Write("SNo Teacher Name       Class    Section   ");
            CursorHelper.gotoxy(10, 7);
            Console.Write("___________________________________________");
            int pos = 8;

            for (int i = 0; i < count; i++)
            {
                CursorHelper.gotoxy(10, pos);
                Console.Write(i + 1);
                CursorHelper.gotoxy(14, pos);
                Console.Write(Teachers_Record[i].Name);
                CursorHelper.gotoxy(33, pos);
                Console.Write(Teachers_Record[i].Class);
                CursorHelper.gotoxy(42, pos);
                Console.Write(Teachers_Record[i].Section);
                pos++;
            }
            CursorHelper.gotoxy(10, pos++);
            Console.Write("___________________________________________");
            pos++;
            CursorHelper.gotoxy(10, pos++);
        }

        private void AddRecords()
        {
            while (true)
            {
                CursorHelper.clrscr();
                CursorHelper.FormatScreen(ConsoleColor.DarkGreen,ConsoleColor.White);
                CursorHelper.gotoxy(10, 4);
                Console.Write("Welcome to Teachers Record Management System");
                CursorHelper.gotoxy(10, 5);
                Console.Write("___________________________________________");
                CursorHelper.gotoxy(10, 6);
                Console.Write("Teacher Name: ");
                CursorHelper.gotoxy(10, 7);
                Console.Write("Class: ");
                CursorHelper.gotoxy(10, 8);
                Console.Write("Section: ");
                CursorHelper.gotoxy(10, 9);
                Console.Write("___________________________________________");
                CursorHelper.gotoxy(23, 6);
                var name = Console.ReadLine();
                CursorHelper.gotoxy(17, 7);
                var cls = Console.ReadLine();
                CursorHelper.gotoxy(23, 8);
                var section = Console.ReadLine();
                AddRecord(name, cls, section);
                CursorHelper.gotoxy(10, 11);
                Console.Write("Do you want to add another record (Y/N)? ");
                char ch = Convert.ToChar(Console.ReadLine());
                if (ch == 'Y' || ch == 'y')
                    continue;
                else
                    break;

            }
        }

        private void EditRecords()
        {
            ViewRecords(ConsoleColor.DarkCyan, ConsoleColor.White);
            Console.Write("Enter the serial number you want to edit: ");
            var recPos = Convert.ToInt32(Console.ReadLine());
            if (recPos >= 1 && recPos <= _maxRecordsCount)
            {
                CursorHelper.clrscr();
                CursorHelper.gotoxy(10, 4);
                Console.Write("Welcome to Teachers Record Management System");
                CursorHelper.gotoxy(10, 5);
                Console.Write("___________________________________________");
                CursorHelper.gotoxy(10, 6);
                Console.Write("Teacher Name: ");
                CursorHelper.gotoxy(10, 7);
                Console.Write("Class: ");
                CursorHelper.gotoxy(10, 8);
                Console.Write("Section: ");
                CursorHelper.gotoxy(10, 9);
                Console.Write("___________________________________________");
                CursorHelper.gotoxy(23, 6);
                var name = Console.ReadLine();
                CursorHelper.gotoxy(17, 7);
                string cls = Console.ReadLine();
                CursorHelper.gotoxy(23, 8);
                string section = Console.ReadLine();
                EditRecord(recPos - 1, name, cls, section);
                CursorHelper.gotoxy(10, 12);
                Console.Write("Record updated. Press any key to return to Main Menu");
                Console.ReadLine();
            }
            else
            {
                CursorHelper.clrscr();
                CursorHelper.gotoxy(10, 12);
                Console.Write("Invalid Entry. Press any key to return to Main Menu");
                Console.ReadLine();
            }
        }

        private void DeleteRecords()
        {
            ViewRecords(ConsoleColor.Red, ConsoleColor.White);
            Console.Write("Enter the serial number you want to delete: ");
            int recNo;
            recNo = Convert.ToInt32(Console.ReadLine());
            if (recNo >= 1 && recNo <= _maxRecordsCount)
            {
                DeleteRecord(recNo - 1);
                Console.Write("          Record deleted. Press any key to return to Main Menu");
                Console.ReadLine();
            }
            else
            {
                CursorHelper.clrscr();
                CursorHelper.gotoxy(10, 12);
                Console.Write("          Invalid Entry. Press any key to return to Main Menu");
                Console.ReadLine();
            }
        }


        //Private read write methods
        private void AddRecord(string name, string cls, string section)
        {
            int current = _maxRecordsCount;
            Teachers_Record[current] = new Teacher {Name = name, Class = cls, Section = section};
            _maxRecordsCount++;
            WriteRecordsInDb();
        }

        private void EditRecord(int recPos,string name, string cls, string section)
        {
            Teachers_Record[recPos].Name = name;
            Teachers_Record[recPos].Class = cls;
            Teachers_Record[recPos].Section = section;
            WriteRecordsInDb();
        }

        private void DeleteRecord(int recPos)
        {
            _maxRecordsCount--;
            for (int i = recPos; i < _maxRecordsCount; i++)
            {
                Teachers_Record[i] = Teachers_Record[i + 1];
            }
            WriteRecordsInDb();
        }
        private int ReadTeacherDbFile()
        {
            try
            {
                BinaryReader inputStream = new BinaryReader(File.Open(_fileName, FileMode.Open));
                if (inputStream == null)
                {
                    return -1;
                }

                Byte[] buffer = new byte[4096];

                int totalRecords = 0;

                for (int i = 0; i < 100; i++)
                {
                    int currentChar = inputStream.PeekChar();

                    if (currentChar == -1) break;
                    Teachers_Record[i] = new Teacher
                    {
                        Name = inputStream.ReadString(),
                        Class = inputStream.ReadString(),
                        Section = inputStream.ReadString()
                    };
                    totalRecords += 1;
                }
                inputStream.Close();
                _maxRecordsCount = totalRecords;
                return totalRecords;
            }
            catch (Exception)
            {

                return -1;
            }
        }

       private int WriteRecordsInDb()
        {
            BinaryWriter outputStream = new System.IO.BinaryWriter(System.IO.File.Open(_fileName, System.IO.FileMode.OpenOrCreate));
            if (outputStream == null)
                return -1;
            int totalRecordsWritten = 0;
            char[] buf = new char[4096];
            for (int i = 0; i < _maxRecordsCount; i++)
            {
                outputStream.Write(Teachers_Record[i].Name);
                outputStream.Write(Teachers_Record[i].Class);
                outputStream.Write(Teachers_Record[i].Section);
                totalRecordsWritten += 1;
            }
            outputStream.Close();
            return totalRecordsWritten;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class CommandParser
    {
        private String commandLine;
        private String[] tokens;
        private String args;
        //private enum Command {NONE, LOAD, ADD, PRT};
        private int command = Command.NONE;

        //建構子
        public CommandParser(String commandLine)
        {
            format(commandLine);
        }

        //將傳入字串的頭尾空白去掉，並將英文轉大寫字母
        private void format(String commandLine)
        {
            commandLine = commandLine.Trim(' ');
            this.commandLine = commandLine.ToUpper();
        }

        public String getCommandLine()
        {
            return commandLine;
        }

        //判斷一個字串是否為正整數的字串。如果是則回傳true，否則回傳false
        private bool checkInteger(String str)
        {
            try
            {
                int num;
                bool result = Int32.TryParse(str, out num);
                if (num <= 0)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //判斷傳入字串中是否全為T語言所定義的字元，且是否以分號結尾。不符上述條件者請回傳0，否則回傳1。
        private int isValid(String commandLine)
        {
            if (commandLine == null)
                return 0;
            this.tokens = this.commandLine.Split(' ');
            if (tokens[0].Equals("LOAD"))
            {
                command = Command.LOAD;
                if (checkTokens() == 2)
                {
                    char[] charArr = tokens[1].ToCharArray();
                    int n = charArr.Length;
                    if (n < 2)
                        return 0;
                    if (charArr[n - 1] == ';')
                    {
                        String num = tokens[1].Substring(0, n - 1);
                        if (checkInteger(num) == true)
                        {
                            args = num;
                            return 1;
                        }
                    }
                }
            }
            else if (tokens[0].Equals("ADD"))
            {
                command = Command.ADD;
                if (checkTokens() == 2)
                {
                    char[] charArr = tokens[1].ToCharArray();
                    int n = charArr.Length;
                    if (n < 2)
                        return 0;
                    if (charArr[n - 1] == ';')
                    {
                        String num = tokens[1].Substring(0, n - 1);
                        if (checkInteger(num) == true)
                        {
                            args = num;
                            return 1;
                        }
                    }
                }
            }

            else if (tokens[0].Equals("PRT;"))
            {
                if (checkTokens() == 1)
                {
                    command = Command.PRT;
                    return 1;
                }
            }
            return 0;
        }

        //假設字串s中包含T語言所定義的合法字元同時以分號結尾。請寫一個方法計算s中字組（token）的個數。
        private int checkTokens()
        {
            int num = tokens.Length;
            return num;
        }

        //請判斷字串s是否滿足T語言的指令格式，如果有錯誤地方請回傳-1。如果指令是LOAD便回傳0，ADD則回傳1，PRT回傳2。如指令後有其他數字，便存到params之中
        public int parser(String s)
        {
            if (isValid(s) == 0)
                return -1;
            else
            {
                if (command == Command.LOAD)
                    return Command.LOAD;
                if (command == Command.ADD)
                    return Command.ADD;
                if (command == Command.PRT)
                    return Command.PRT;
            }
            return -1;
        }

        public int getCommand()
        {
            return command;
        }

        public String getArgs()
        {
            return args;
        }
    }
}

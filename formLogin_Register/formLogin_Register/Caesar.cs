using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formLogin_Register
{
    class Caesar
    {
        string key = "aáàạảãăắằặẳẵâấầậẩẫbcdđeéẹẻẽêếềệểễfghiíìịỉĩjklmnoóòọỏõôốồộổỗơớờợởỡpqrstuúùụủũưứừựửữvwxyýỳỵỷỹAÁÀẠẢÃĂẮẰẶẲẴÂẤẦẬẨẪBCDĐEÉẸẺẼÊẾỀỆỂỄFGHIÍÌỊỈĨJKLMNOÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠPQRSTUÚÙỤỦŨƯỨỪỰỬỮVWXYÝỲỴỶỸ0123456789`~!@#$%^&*()";

        private int index(string str, char x)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == x)
                    return i;
            }
            return -1;
        }

        public string encrypt(string plaintext, int n)
        {
            string res = "";

            for (int i = 0; i < plaintext.Length; i++)
            {
                int t = index(key, plaintext[i]);
                if (t != -1)
                {
                    int t2 = (t + n) % key.Length;
                    res += key[t2];
                }
                else
                {
                    res += plaintext[i];
                }
            }

            return res;
        }

        public string decrypt(string ciphertext, int n)
        {
            string res = "";

            for (int i = 0; i < ciphertext.Length; i++)
            {
                int t = index(key, ciphertext[i]);
                if (t != -1)
                {
                    int t2 = (t - n) % key.Length;
                    res += key[t2];
                }
                else
                {
                    res += ciphertext[i];
                }
            }

            return res;
        }
    }
}

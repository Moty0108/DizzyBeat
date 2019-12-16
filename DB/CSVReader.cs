using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace TH
{

    public class CSVReader
    {
        static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        static char[] TRIM_CHARS = { '\"' };

        public static List<Dictionary<string, object>> Read(string file)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            TextAsset data = Resources.Load(file) as TextAsset;;
            Debug.Log(data.text);

            string[] lines = Regex.Split(data.text, LINE_SPLIT_RE); // 개행으로 구분해서 최대 줄 수와 데이터를 받아옴
            Debug.Log("데이터 개수" + lines.Length);

            if (lines.Length <= 1)  // 줄이 없으면 그대로 반환
            {
                Debug.Log("CSV 데이터 없음");
                return list;
            }

            string[] header = Regex.Split(lines[0], SPLIT_RE);  // 헤더를 읽어옴

            for (int i = 1; i < lines.Length; i++)  // 데이터부분을 읽어옴
            {
                string[] values = Regex.Split(lines[i], SPLIT_RE);

                if (values.Length == 0 || values[0] == "")
                    continue;

                Dictionary<string, object> entry = new Dictionary<string, object>();

                for (int j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");  // 앞뒤 공백 제거
                    object finalvalue = value;
                    int n;
                    float f;


                    if (int.TryParse(value, out n))  // 정수면 정수로 저장
                    {
                        finalvalue = n;
                    }
                    else if (float.TryParse(value, out f))   // 실수면 실수로 저장
                    {
                        finalvalue = f;
                    }
                    // 해당사항 없으면 문자열 그대로 저장

                    entry[header[j]] = finalvalue;  // 헤더(이름) 딕셔너리에 값 저장
                }
                list.Add(entry);
            }

            return list;
        }
    }
}
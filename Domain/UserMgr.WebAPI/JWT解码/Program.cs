using System.Text;

//未被篡改
//string s = @"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJQYXNzcG9ydCI6IjEyMzQ1NiIsIlFRIjoiODg4ODg4IiwiSWQiOiI2NjY2IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxMTExMTEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoieXprIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvaG9tZXBob25lIjoiOTk5IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIm1hbmFnZXIiLCJhZG1pbiJdLCJleHAiOjE2NzI1ODA0NTN9.20JhuK51o8_f5YspK9_b5Gy9w3JDxvbOghh0ku5viBU";
//已被篡改
string s = @"1yJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJQYXNzcG9ydCI6IjEyMzQ1NiIsIlFRIjoiODg4ODg4IiwiSWQiOiI2NjY2IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxMTExMTEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoieXprIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvaG9tZXBob25lIjoiOTk5IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIm1hbmFnZXIiLCJhZG1pbiJdLCJleHAiOjE2NzI1ODA0NTN9.20JhuK51o8_f5YspK9_b5Gy9w3JDxvbOghh0ku5viBU";
string[] str = s.Split(".");
string header = str[0];
string payload = str[1];
string sig = str[2];

Console.WriteLine(JwtDecode(header));
Console.WriteLine(JwtDecode(payload));
Console.WriteLine(JwtDecode(sig));

string JwtDecode(string s){
    s = s.Replace('-', '+').Replace('_', '/');
    switch (s.Length % 4)
    {
        case 2:
            s += "==";
            break;
        case 3:
            s += "=";
            break;
    }
    var bytes = Convert.FromBase64String(s);
    return Encoding.UTF8.GetString(bytes);}
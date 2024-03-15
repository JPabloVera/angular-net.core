namespace Token_API.Models;

public class HTTPResponse{

    public class TokenData{
        public string status { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
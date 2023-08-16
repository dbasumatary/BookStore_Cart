namespace BookStoreCart.Entity
{
    public class ResponseEntity
    {
        public object Data { get; set; }  //json object
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Execution successfull";
    }
}

app.service("clipboard", function($window){
    this.copy = function(text){
        this.type = "text/plain";
        this.blob = new Blob([text], { type: this.type });
        this.data = [new ClipboardItem({[this.blob.type]: this.blob})]
        $window.navigator.clipboard.write(this.data);
    }
})
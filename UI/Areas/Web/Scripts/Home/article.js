$(function(){
    getText();
})

function getText(){
    var articleId=$(".ID p").text();
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/GetHtml",
        data: {'Id':articleId},
        dataType: "Json",
        success: function (data) {
            $(".article-title h2").text()=data.Title;
            $(".artiucle-text").append(data.Content);
            var str="posted*"+data.CreateTime+" 薄荷灬少年"+" 阅读（"+data.WatchCount+") 评论（"+data.CommitCount+")"
            $(".bolg-block p").text()=str;
        } 
    });
}
$(function(){
    //获取文章列表
    getArticleList();

    $(".recommend").click(function(){
        var id=  $(this).attr("id");
    });
})

function getArticleList(){
    $.ajax({
        type: "Get",
        url: "url",
        data: "",
        dataType: "Json",
        success: function (data) {
            var count = data.length;
            for(var i=0;i<count;i++){
                var str;

                str=" <li><div class='blog-a'><div class='recommend'"+" "+" id='"+i+"'> <span>"+
                data[i].ZanCount+"</span></div><div class='title'><h2>"+data[i].Title+
                "</h2></div></div><div class='summary'><p>"+data[i].Content+"</p><span><a href='"+
                data[i].address+"'>阅读全文</a></span> </div><div class='bolg-block'><p>posted*"+
                data[i].CreateTime+"薄荷灬少年 阅读("+data[i].WatchCount+") 评论("+data[i].Comment+
                ")</p>"
                $(".article-list").append(str);
            }
        }
    });
}

function addZan(data){
    $.ajax({
        type: "Post",
        url: "url",
        data: {'textid':data},
        dataType: "dataType",
        success: function (data) {
            alert(data.Msg);
            var str;
            str = "#"+data+""+"span";
            $(str).val()=data.data;
            
        },
        error:function(){
            alert("每篇文章每个用户只能点赞一次")
        }
    });
}
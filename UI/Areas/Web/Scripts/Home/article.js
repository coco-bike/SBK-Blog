$(function(){
    //获取文本
    getText();

    //获取评论
    getCommit();
})

//获取文本
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

//获取文章评论
function getCommit(){
    var articleId=$(".ID p").text();
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/GetCommentList",
        data: {'articleId':articleId},
        dataType: "Json",
        success: function (data) {
            var str="";
            var count = data.length;
            if(count>0){

                for( var i=0;i<count;i++)
                {
                    str +="<li id='li"+data[i].Id+"'>"+"<div class='commitmessage'><p>"+i+"楼<span>"+" "+data[i].CreateTime+" </span>"
                    +data[i].UserData+" <a href='javascript:void(0);' onclick='editCom("+data[i].Id
                    +")'>修改</a> <a href='javascript:void(0);' onclick='deleteCom("+data[i].Id+
                    ")'>删除</a></p><p>"+data[i].Content+"</p></div></li>"
                }
                $(".block-1 ul").append(str);                
            }else
            {
                str = "<li><p>暂无评论</p></li>"
                $(".block-1 ul").append(str);
            }      
         
        }
    });
}

//提交评论
function postCommit(){
    var committext = $("#committext").text();
    var articleId=$(".ID p").text();
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/Postcomment",
        data: {'committext':committext,'articleId':articleId},
        dataType: "Json",
        success: function (data) {
            var str="";
            str ="<li id='li"+data.Id+"'>"+"<div class='commitmessage'><p>"+i+"楼<span>"+" "+data.CreateTime+" </span>"
            +data[i].UserData+" <a href='javascript:void(0);' onclick='editCom("+data.Id
            +")'>修改</a> <a href='javascript:void(0);' onclick='deleteCom("+data.Id+
            ")'>删除</a></p><p>"+data.Content+"</p></div></li>"
            $(".block-1 ul").append(str);
        }
    });
}

var commitId="";
var commitContent="";
//修改评论
function editCom(Id){
    $("#submit").css("display","none");
    $("#save").css("display","block");
    $("#cancel").css("display","block");
    commitContent=$("#committext").text();
    commitId = Id;
    var str = "#il"+Id+" .commitmessage p:last";
    $("#committext").text()=$(str).text();
}

//删除评论
function deleteCom(Id){
    var message=confirm("你确定删除吗？");
    if(message==true){
        deleteCommit(Id);
    }
    else if(message==false){
        return;
    }
}

//保存的修改的评论
function saveCommit(){
    var committext =  $("#committext").text();
    if(committext!=commitContent){
        $.ajax({
            type: "Post",
            url: "../../../Web/Home/SaveEditComment",
            data: {'committext':committext,'commitId':commitId},
            dataType: "Json",
            success: function (data) {
                $("#submit").css("display","block");
                $("#save").css("display","none");
                $("#cancel").css("display","none");
                var str1 = "#il"+commitId+" .commitmessage p:first span";
                var str2 = "#il"+commitId+" .commitmessage p:last";
                $(str1).text()=data.CreateTime;
                $(str2).text()=data.Content;
            }
        });
    }
    else{
        alert("你并未进行修改");
    }
   
}


//取消修改的评论
function cancelCommit(){
    $("#committext").text()="";
    $("#submit").css("display","block");
    $("#save").css("display","none");
    $("#cancel").css("display","none");
}

//删除评论方法
function deleteCommit(Id){
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/DestoryCommment",
        data: {"commitId":Id},
        dataType: "Json",
        success: function (data) {
            alert(data.Msg);
            var str1 = "#il"+commitId;
            $(str1).css('display','none');
        }
    });
}
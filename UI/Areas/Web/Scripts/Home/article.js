$(function(){
    //获取文本
    getText();

    //获取评论
    getCommit();


    //绑定点赞
    $(".zan").click(function(){

        //增加点赞
        addZan();
    })
})

//获取文本
function getText(){
    var articleId=$(".zan").attr('id');
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/GetHtml",
        data: {'Id':articleId},
        dataType: "Json",
        success: function (data) {
            $(".article-title h2").text(data.Data.Title);
            $(".article-text").append(data.Data.Content);
            var str="posted*"+data.Data.UpdateTime+" 薄荷灬少年"+" 阅读（"+data.Data.WatchCount+") 评论（"+data.Data.CommitCount+")"
            $(".bolg-block p").text(str);
            $(".zan div").text(data.Data.ZanCount);
        } 
    });
}

//获取文章评论
function getCommit(){
    var articleId=$(".zan").attr('id');
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/GetCommentList",
        data: {'Id':articleId},
        dataType: "Json",
        success: function (data) {
            var str="";
            var count = data.Data.length;
            var mydata = data.Data;
            if(count>0){

                for( var i=0;i<count;i++)
                {
                    str +="<li id='li"+mydata[i].Id+"'>"+"<div class='commitmessage'><p>"+(i+1)+"楼 | <span>"+" "+mydata[i].UpdateTime+" </span><span class='name'>"
                    +mydata[i].UserData+" </span><a href='javascript:void(0);' onclick='replyCom("+mydata[i].Id
                    +")'>回复</a> <a href='javascript:void(0);' onclick='quoteCom("+mydata[i].Id+
                    ")'>引用</a></p><p id='content"+mydata[i].Id+"'>"+mydata[i].Content+"</p></div></li>";
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

var fartherid=0;
//提交评论
function postcomment(){
    var commenttext = $("#commenttext").val();
    var articleId=$(".zan").attr('id');
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/Postcomment",
        data: {'commenttext':commenttext,'articleId':articleId,'commentfartherid':fartherid},
        dataType: "Json",
        success: function (data) {
            var str="";
            var mydata = data.Data;
            str ="<li id='li"+mydata.Id+"'>"+"<div class='commitmessage'><p>"+mydata.CommentCount+"楼 | <span>"+" "+mydata.UpdateTime   +"</span><span class='name'>"
            +mydata.UserName+"</span><a href='javascript:void(0);' onclick='replyCom("+mydata.Id
            +")'>回复</a> <a href='javascript:void(0);' onclick='quoteCom("+mydata.Id+
            ")'>引用</a></p><p id='content"+mydata.Id+"'>"+mydata.Content+"</p></div></li>";

            $(".block-1 ul").append(str);
            $("#commenttext").val("");
        }
    });
}


function replyCom(id){
    $("#commenttext").val("");
    var str ="#li"+id+" .name";
    var content="@"+$(str).text()+":";
    $("#commenttext").val(content);
    window.location.href="#commenttext";
    fartherid=id;
}

function quoteCom(id){
    $("#commenttext").val("");
    var str ="#content"+id;
    var content ="引用:"+id+"楼  "+$(str).text()+"  我的评论:"
    $("#commenttext").val(content);
    window.location.href="#commenttext";
    fartherid=0;
}
// var commentId="";
// var commentContent="";

// //修改评论
// function editCom(Id){
//     $("#submit").css("display","none");
//     $("#save").css("display","block");
//     $("#cancel").css("display","block");
//     commentContent=$("#commenttext").text();
//     commentId = Id;
//     var str = "#il"+Id+" .commitmessage p:last";
//     $("#commenttext").text()=$(str).text();
// }

// //删除评论
// function deleteCom(Id){
//     var message=confirm("你确定删除吗？");
//     if(message==true){
//         deleteCommit(Id);
//     }
//     else if(message==false){
//         return;
//     }
// }

// //保存的修改的评论
// function saveCommit(){
//     var committext =  $("#committext").text();
//     if(committext!=commitContent){
//         $.ajax({
//             type: "Post",
//             url: "../../../Web/Home/SaveEditComment",
//             data: {'committext':committext,'commitId':commitId},
//             dataType: "Json",
//             success: function (data) {
//                 $("#submit").css("display","block");
//                 $("#save").css("display","none");
//                 $("#cancel").css("display","none");
//                 var str1 = "#il"+commitId+" .commitmessage p:first span";
//                 var str2 = "#il"+commitId+" .commitmessage p:last";
//                 $(str1).text()=data.CreateTime;
//                 $(str2).text()=data.Content;
//             }
//         });
//     }
//     else{
//         alert("你并未进行修改");
//     }
   
// }


//取消修改的评论
function cancelcomment(){
    $("#commenttext").val("");
}

// //删除评论方法
// function deleteCommit(Id){
//     $.ajax({
//         type: "Post",
//         url: "../../../Web/Home/DestoryCommment",
//         data: {"commitId":Id},
//         dataType: "Json",
//         success: function (data) {
//             alert(data.Msg);
//             var str1 = "#il"+commitId;
//             $(str1).css('display','none');
//         }
//     });
// }

//增加点赞的方法
function addZan() {
    var articleId=$(".zan").attr('id');
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/AddZan",
        data: {
            'id': articleId
        },
        dataType: "Json",
        success: function (data) {
            alert(data.Msg);
            var str;
            str = "#bolg-message" + " span"+" div";
            $(str).text(data.data);

        },
        error: function () {
            alert("每篇文章每个用户只能点赞一次")
        }
    });
}
var max = 0;
var size = 5;
var page_now = 1;

/*
 * Page Demo v 1.0.0
 * Copyright 2015-08-03 Jane
 * page_now 当前页码，初始值为0
 * page_max 最大页码，初始值为0
 * calculate_page(page_new) 重新计算当前的显示，page_new代表新的页码
 * go_to_page() 用户输入页码之后调用的函数，判断用户输入并调用重新计算页码显示
 */

jQuery.support.cors = true;

//分页
function getMessage(page_now) {
    $.ajax({
        type: "post",
        url: "../../../Web/Home/GetArticleList",
        data: {
            "pagenow": page_now,
            "pagesize": size
        },
        dataType: "Json",
        success: function (data) {
            $('#article-list').empty();
            max = data.totalpage;
            var mydata = new Array()
            mydata = data.listdata;
            var s = "";
            for (var i = 0; i < mydata.length; i++) {
                s += " <li><div class='blog-a'><div class='recommend'" + " " + " id='" + data[i].Id + "'> <span>" +
                    data[i].ZanCount + "</span></div><div class='title'><h2>" + data[i].Title +
                    "</h2></div></div><div class='summary'><p>" + data[i].Summary + "</p><span><a href='javascript:void(0);' " +
                   "onclick='readarticle("+data[i].Id + ")'>阅读全文</a></span> </div><div class='bolg-block'><p>posted* "+
                    data[i].CreateTime+" 薄荷灬少年 阅读(" + data[i].WatchCount + ") 评论(" + data[i].CommentCount +")</p>";                
            }
            $('#article-list').append(s);
            page.page_max = max;
            page.calculate_page(page_now); //page.calculate_page(当前页)
        }
    });
};

$(function () {
    //获取文章列表
    getMessage(page_now);

    //绑定点击事件
    $(".recommend").click(function () {
        var id = $(this).attr("id");

        addZan(id);
    });
})

//点赞功能
function addZan(Id) {
    $.ajax({
        type: "Post",
        url: "../../../Web/Home/AddZan",
        data: {
            'id': Id
        },
        dataType: "dataType",
        success: function (data) {
            alert(data.Msg);
            var str;
            str = "'#" + Id + " " + "span'";
            $(str).text() = data.data;

        },
        error: function () {
            alert("每篇文章每个用户只能点赞一次")
        }
    });
}

function readarticle(Id){
    window.location.href="../../../Web/Home/Aritle?"+"id="+Id;
}
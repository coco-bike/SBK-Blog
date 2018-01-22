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

//api跨域
jQuery.support.cors = true;

//获取文章列表
function getMessage(page_now) {

    //ajax请求
    $.ajax({
        type: "post",
        url: "../../../Management/GetArticleList",
        data: {
            "pagenow": page_now,
            "pagesize": size
        },
        dataType: "Json",
        //成功后的回调函数
        success: function (data) {
            //表格置空
            $('#tbody').empty();
            //获取总页面
            max = data.totalpage;
            var mydata = new Array();
            //获取文章列表
            mydata = data.listdata;
            //字符串置空
            var s = " ";
            //循环添加表格列
            for (var i = 0; i < mydata.length; i++) {
                s = "<tr><th>"+mydata[i].Title+"</th><th>"+mydata[i].Time+"</th><th>"
                +mydata[i].State+"</th><th>"+mydata[i].WatchCount+"</th><th><a"+" "+"href='"
                +"javascript:void(0);'"+" onclick='Edit("+mydata[i].Id+")'>"+"编辑</a></th><th><a"
                +" "+"href='"+"javascript:void(0);'"+" onclick='Destory("+mydata[i].Id+")'>"
                +"删除</a></th></tr>";
            }
            //表格添加
            $('#tbody').append(s);
            //赋值给插件最大页码
            page.page_max = max;
            //加载当前页
            page.calculate_page(page_now); //page.calculate_page(当前页)
        }
    });
};

//修改文章
function Edit(Id){
    var url = "../../../Article/EditArticle"
    window.location.href =url+"?Id="+Id;
}

//删除文章
function Destory(Id){
    var url =  "../../../Article/DestoryArticle"
    $.ajax({
        type: "Post",
        url: url,
        data: {"Id":Id},
        dataType: "Json",
        success: function (data) {
            if(data.status==1){
                alert("删除成功");
            }
        }
    });
}

$(function(){
    //获取文章列表
    getMessage(page_now);
})
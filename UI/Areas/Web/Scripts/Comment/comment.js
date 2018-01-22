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
        url: "../../../Comment/GetCommentList",
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
                s = "<tr><th>"+mydata.Title+"</th><th>"+mydata.Time+"</th><th><a"+
                " href='javascript:void(0);'"+" onclick='destory("+mydata.Id+")'>"+
                "删除</a></th><tr>"+"<tr><th>"+mydata.Content+"</th></tr>";
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

//删除评论
function destory(Id){
    $.ajax({
        type: "Post",
        url: "../../../Comment/DestoryComment",
        data: {"Id":Id},
        dataType: "Json",
        success: function (data) {
            alert(data.Msg);
        }
    });
}

$(function(){
    //获取文章列表
    getMessage(page_now);
})
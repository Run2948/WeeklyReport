//Layer弹出组件的动画方式
var _layerShift = 5;

function goUrl(url)
{
    window.location.href = url;
}

function getQueryString(name)
{
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//Cookie 操作
function setCookie(name, value, expiredays)
{

    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)

    var str = name + "=" + escape(value);
    if (expiredays != null)
    {
        str += ";expires=" + exdate.toGMTString();
    }

    document.cookie = str;
}

function getCookie(name)
{
    if (document.cookie.length > 0)
    {
        c_start = document.cookie.indexOf(name + "=")
        if (c_start != -1)
        {
            c_start = c_start + name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}

function removeCookie(name)
{
    var exdate = new Date()
    exdate.setDate(exdate.getDate() - 10);
    document.cookie = name + "=v; expires=" + exdate.toGMTString();
}

////////

//Layer

function layerInputAlertMsg()
{
    layerMsg("请核对您的输入。");
}

function layerMsg(message,time)
{
    if (time != undefined && time != null)
    {
        layer.msg(message, {
            time: time
        });
    }
    else
    {
        layer.msg(message, {
            time: 1500
        });
    }
}

function layerAlert(message,callback)
{
    var alertLayerIndex = layer.alert(message, {
        title: false, closeBtn: false, shift: _layerShift,
        success: function (layero, index)
        {
            $(layero).focus();
            $(layero).keypress(function (e)
            {
                if (e.keyCode == 13)
                {
                    layer.close(alertLayerIndex);
                    if (callback != undefined && callback != null)
                    {
                        callback();
                    }
                } else if (e.keyCode == 27)
                {
                    layer.close(alertLayerIndex);
                    if (callback != undefined && callback != null)
                    {
                        callback();
                    }
                }
            });
            ////alert($(layero).find("a").length);
            //alert($($(layero).find("a")[0]).html());
            //$($(layero).find("a")[0]).focus();
        },
        yes: function (index)
        {
            layer.close(alertLayerIndex);
            if (callback != undefined && callback != null)
            {
                callback();
            }
        }
    });
}

///////////

//维持session
function heartbeat()
{
    setInterval(function ()
    {
        $.get("/Api/UserContext/Heartbeat");
    },60000);    
}
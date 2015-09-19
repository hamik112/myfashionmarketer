var strength = "";
function register() {

    try {
        var email = $('#txtrEmail').val();
        var password = $('#txtrPassword').val();
        var firstname = $('#txtrFirstName').val();
        var lastname = $('#txtrLastName').val();
        var confirmpassword = $('#txtrConfirmPassword').val();
        var packag = $("#package :selected").val();
        var company = $('#txtrCompany').val();
        var Phone = $('#txtrPhone').val();
        console.log(company);
        var Lower_FirstName = firstname.toLowerCase();
        var Lower_LastName = lastname.toLowerCase();
        if (packag != '') {

            if (firstname != '') {

                if (validateFName(firstname)) {

                    if (lastname != '') {

                        if (validateLName(lastname)) {

                            if (email != '') {

                                if (validateEmail(email)) {

                                    if (Lower_FirstName != Lower_LastName) {

                                        if (password != '') {

                                            if (confirmpassword != '') {

                                                if (password == confirmpassword) {

                                                    if (strength != "weak") {

                                                        var totaldata = "{'Phone':'" + Phone + "', 'email':'" + email + "', 'password': '" + password + "' , 'firstname':'" + firstname + "', 'lastname':'" + lastname + "','Company': '" + company + "' , 'package':'" + packag + "' }";

                                                        $.ajax({
                                                            type: "POST",
                                                            url: "../Index/Register",
                                                            data: totaldata,
                                                            contentType: "application/json; charset=utf-8",
                                                            success: function (msg_Signup) {

                                                                if (msg_Signup == "Email Already Exists") {

                                                                    alert("Email Already Exists")
                                                                    return;
                                                                }
                                                                else if (msg_Signup == "Success") {
                                                                    alert1(email, password);
                                                                   // window.location = "../Home/Index"
                                                                }
                                                                else {

                                                                    window.location = "../Index/Index"
                                                                }

                                                            }

                                                        });

                                                    } else {
                                                        alert("Password should contain atleast 6 characters and should be alphanumeric");
                                                        return;
                                                    }
                                                } else {
                                                    alert("Password and Confirm Password is not Matched");
                                                    return;
                                                }
                                            } else {
                                                alert("Enter Confirm Password");
                                                return;
                                            }
                                        } else {
                                            alert("Enter Password");
                                            return;
                                        }
                                    } else {
                                        alert("First Name and Last Name Can not be same!");
                                        return;
                                    }

                                } else {
                                    alert("Invalid Email");
                                    return;
                                }
                            } else {
                                alert("Enter Email");
                                return;
                            }
                        } else {
                            alert("Invalid Last Name");
                            return;
                        }

                    } else {
                        alert("Enter Last Name");
                        return;
                    }

                } else {
                    alert("Invalid First Name");
                    return;
                }
            } else {
                alert("Enter First Name");
                return;
            }
        } else {
            alert("Select Account Type");
            return;
        }
    } catch (e) {

    }
}


function password() {

    var strongRegex = new RegExp("^(?=.{7,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
    var mediumRegex = new RegExp("^(?=.{5,})(((?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
    if ($('#txtrPassword').val() != "") {
        $('#password_strength').css('display', 'block');
        $('#_password_strength').css('display', 'block');
        if ($('#txtrPassword').val().length <= 15) {
            if (strongRegex.test($('#txtrPassword').val())) {
                $('#stregth').html('<b>Strong!</b>');
                $('#weak').css("background-color", "#006400");
                $('#medium').css("background-color", "#006400");
                $('#strong').css("background-color", "#006400");
                strength = "";
            } else if (mediumRegex.test($('#txtrPassword').val())) {
                $('#stregth').html('<b>Medium!</b>');
                $('#weak').css("background-color", "#FFFF00");
                $('#medium').css("background-color", "#FFFF00");
                $('#strong').css("background-color", "rgb(204, 204, 204)");
                strength = "";
            } else {
                $('#stregth').html('<b>Weak!</b>');
                strength = "weak";
                $('#weak').css("background-color", "#FF0000");
                $('#medium').css("background-color", "rgb(204, 204, 204)");
                $('#strong').css("background-color", "rgb(204, 204, 204)");
            }
        } else {
            var pass = $('#txtrPassword').val();
            $('#txtrPassword').val(pass.substring(0, 15));
        }
    }
    else {
        $('#stregth').html('<b>Weak!</b>');
        $('#weak').css("background-color", "#FF0000");
        $('#medium').css("background-color", "rgb(204, 204, 204)");
        $('#strong').css("background-color", "rgb(204, 204, 204)");
        $('#password_strength').css('display', 'none');
        $('#_password_strength').css('display', 'none');
    }
    return true;
}

function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test($email)) {
        return false;
    } else {
        return true;
    }
}

function validateFName($Fname) {
    //var fnameReg = /^[A-Z,a-z._]+$/;
    //regular expression for accept numbers and characters
    var fnameReg = /^[a-zA-Z0-9_]*$/;
    if (!fnameReg.test($Fname)) {
        return false;
    } else {
        return true;
    }
}

function validateLName($lname) {
    //var lnameReg = /^[A-Z,a-z._]+$/;
    var lnameReg = /^[a-zA-Z0-9_]*$/;
    if (!lnameReg.test($lname)) {
        return false;
    } else {
        return true;
    }
}

function validatePhone($Phoneno) {
    var phoneReg = /^\d{10}$/;
    if (!phoneReg.test($Phoneno)) {
        return false;
    } else {
        return true;
    }
}

function signinFunction() {
    debugger;
    try {
        var password = document.getElementById('txtPassword').value;


        var username = document.getElementById('txtEmail').value;
        if (password != '' && password != undefined) {
        }
        if (username != '' && password != '' && username != undefined && password != undefined) {

            $.ajax({
                type: 'POST',

                url: '../Index/SignIn?&username=' + encodeURIComponent(username) + '&password=' + encodeURIComponent(password),
                success: function (msg) {
                    debugger;
                    if (msg != "Invalid Email or Password") {
                        if ($("#RememberMe").is(':checked')) {
                            checkCookie(username, password);
                        }
                    }
                    if (msg == "user") {
                        alert1(username, password);
                    }

                    else if (msg == "User Not Exist!") {
                        alert('User Not Exist');
                    }
                    else {

                        window.top.location = "../Index/Login";
                    }
                }

            });

        } else { alert("All fields are mandatory") }

    } catch (e) {

    }

}


function setCookie(cemail, cpwd, exdays) {
    debugger;
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = "logininfo" + cemail + "=" + cpwd + ";" + expires;
}

function GetAllCookies() {
    var clist = [];
    var j = 0;
    debugger;
    var Cookies = document.cookie.split(';');
    for (var i = 0; i < Cookies.length; i++) {
        var currentcooki = Cookies[i];
        if (currentcooki.indexOf("logininfo") > -1) {
            clist[j] = currentcooki;
            j++;
        }
    }
    return clist[0].replace("logininfo", "");
}
function getCookie(cemail) {
    debugger;
    var name = cemail + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) != -1) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie(username, password) {
    debugger;
    var check = getCookie("logininfo" + username);
    if (check != "") {

    } else {
        if (username != "" && username != null && password != "" && password != null) {
            setCookie(username, password, 90);
        }
    }
}


function addaccount() {
    var business_name = $('#txt_bname').val();
    var web_url = $('#txt_weburl').val();
    var default_no = $('#txt_defaultno').val();
    var billing_email = $('#txt_billingemail').val();
    var spamconfirming_url = $('#txt_spamconfurl').val();
    var blocked_url = $('#txt_blockedconfurl').val();
    var google_analytic_id = $('#txt_googleanalId').val();
    var goole_captcha = $('#txt_googlecaptchakey').val();
    var google_secret = $('#txt_googlecaptchasecret').val();
    var default_email = $('#txt_Defaultemail').val();
    var Address = $('#txt_address').val();
    var City = $('#txt_city').val();
    var Postal = $('#txt_postal').val();
    if (business_name != "") {
        var totaldata = "{'Postal':'" + Postal + "','City':'" + City + "','Address':'" + Address + "', 'Company':'" + business_name + "', 'web_url': '" + web_url + "' , 'default_no':'" + default_no + "', 'billing_email':'" + billing_email + "' }";

        $.ajax({
            type: "POST",
            url: "/Assets/addaccount",
            data: totaldata,
            contentType: "application/json; charset=utf-8",
            success: function (msg_Signup) {

                if (msg_Signup == "Email Already Exists") {

                    alert("Email Already Exists")
                    return;
                }
                else if (msg_Signup == "Success") {

                    $.ajax({

                        type: "POST",
                        url: "/Assets/EditAccount?value=addEdit",
                        contentType: "application/json; charset=utf-8",
                        success: function (msg_Signup) {
                            employeeload();

                            $('#partialview').hide();
                            $('#page-container').append(msg_Signup);


                        }
                    })

                }
                else {

                    //          window.location = "../Index/Index"
                }

            }

        });


    }
    else {

    }


}


//$(document).ready(function () {
//    $("#submit").click(function () {
//        alert($("#txtbname").val());

//    });
//});


function updateaccount(Accountid) {

    var bus_name = $('#txtbname').val();
    var web_url = $('#txtweburl').val();
    var default_no = $('#txtdefaultno').val();
    var billing_email = $('#txtbillingemail').val();
    var adress = $('#txtaddress').val();
    var city = $('#txtcity').val();
    var postal = $('#txtpostal').val();
    var spam_url = $('#txtspamconfurl').val();
    var blocked_url = $('#txtblockedconfurl').val();
    var page_domain = $('#txtpagedomain').val();
    var addword = $('#txtaddwordId').val();
    var link_domain = $('#txtlinkdomain').val();
    var google_captcha_key = $('#txtgooglecaptchakey').val();
    var google_captcha_secret = $('#txtgooglecaptchasecret').val();
    //console.log('buisness_name'+ bus_name);
    if (bus_name != "") {
        var totaldata = "{'google_captcha_secret':'" + google_captcha_secret + "','google_captcha_key':'" + google_captcha_key + "','link_domain':'" + link_domain + "','addword':'" + addword + "','page_domain':'" + page_domain + "','spam_url':'" + spam_url + "','blocked_url':'" + blocked_url + "','Postal':'" + postal + "','City':'" + city + "','Address':'" + adress + "', 'Company':'" + bus_name + "', 'web_url': '" + web_url + "' , 'default_no':'" + default_no + "', 'billing_email':'" + billing_email + "' ,'account_id':'" + Accountid + "'}";

        $.ajax({

            type: "POST",
            url: "/Assets/UpdateAccount",
            data: totaldata,


            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                if (msg == "updated") {

                    alert("updated");
                }

            }

        })
    }
}
function editaccount(company_id) {
    company_id = "{'company_id':'" + company_id + "'}"
    $.ajax({

        type: "POST",
        url: "/Assets/EditAccount?value=Edit",
        data: company_id,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {

            $('#page-content-wrapper').hide();
            $('#page-container').append(msg);
            employeeload();
        }
    })



}

function employeeload() {

    $.ajax({
        type: "POST",
        url: "/Assets/Employees",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {

            $('#empbody').append(msg);

        }

    })

}

function addemployee() {
    var emp_first_name = $('#emp_first_name').val();
    var emp_last_name = $('#emp_last_name').val();
    var emp_email = $('#emp_email').val();
    var emp_phone = $('#emp_phone').val();
    if (emp_first_name != "") {
        if (emp_email != "") {
            totaldata = "{'First_name':'" + emp_first_name + "','Last_name':'" + emp_last_name + "','Email':'" + emp_email + "','Phone':'" + emp_phone + "'}"
            $.ajax({
                type: "POST",
                data: totaldata,
                url: "/Assets/AddEmployee",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                    if (msg == "success") {
                        $("#gridSystemModal").css("display", "none");
                        $('#empbody').append("<tbody><tr><td>'" + emp_first_name + "'</td><td>'" + emp_email + "'</td><td>'" + emp_phone + "'</td></tbody>")



                    }

                }

            })
        }
        else {
            alert("Enter valid Email_id");

        }
    }
    else {
        alert("Enter First_name");
    }

}

function updateprofile(Id) {


}

function returnfunction(data) {
    alert(data);
}

function SubmitCustomerForm() {



}

function test() {

    alerta("hello");
}
function globustrackerlogin(uid, pass) {
    alert("fsdf");
    $.ajax({
        type: "POST",
        crossDomain: true,
        dataType: 'jsonp',
        jsonpCallback: "func",
        url: "http://globus.tracker.com/myfashion/loginService.action?username=" + id + "&password=" + puid + "&callback=?",
        contentType: "application/json; charset=utf-8",
        //   contentType: "application/json",
        success: function (json) {
            if (json.code === 101) {
                //      window.location = "/home/globusTracker";

            }
        },
        error: function (e) {
            console.log("abc");
            console.log(e);
        }
    })
}
function alert1(id, puid) {

    //console.log("styart");
    $.ajax({
        type: "POST",
        crossDomain: true,
        dataType: 'jsonp',
        jsonpCallback: "func",
        url: "http://tracker.myfashionmarketer.com/myfashion/loginService.action?username=" + id + "&password=" + puid + "&callback=?",
        contentType: "application/json; charset=utf-8",
        //   contentType: "application/json",
        success: function (json) {
            if (json.code === 101) {

                window.location = "/home/index";

            }
        },
        error: function (e) {
            console.log("abc");
            console.log(e);
        }
    })
}
function track(id, puid) {

    //console.log("styart");
    $.ajax({
        type: "POST",
        crossDomain: true,
        dataType: 'jsonp',
        jsonpCallback: "func",
        url: "http://tracker.myfashionmarketer.com/myfashion/loginService.action?username=" + id + "&password=" + puid + "&callback=?",
        contentType: "application/json; charset=utf-8",
        //   contentType: "application/json",
        success: function (json) {
            if (json.code === 101) {

                window.location = "/home/globustracker";

            }
        },
        error: function (e) {
            console.log("abc");
            console.log(e);
        }
    })
}

$('.logout').on('click', function () {

    alert("Test");
})

function logout() {
    $.ajax({
        type: "POST",
        crossDomain: true,
        dataType: 'jsonp',
        jsonpCallback: "func",
        url: "http://tracker.myfashionmarketer.com/myfashion/logOutService.action?&callback=?",
        contentType: "application/json; charset=utf-8",
        //   contentType: "application/json",
        success: function (json) {
            if (json.code === 101) {

                $.ajax({
                    type: "POST",

                    url: "/home/logout",
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {

                        if (msg == "success") {

                            window.location = "/index/index";

                        }

                    }

                })

            }
        },
        error: function (e) {
            console.log("abc");
            console.log(e);
        }
    })

}

$('.seotracker').on('click', function () {
    var puid = $(this).attr('attrpass');
    var id = $(this).attr('attrid');
    var module = $(this).attr('seocampaign');
    $.ajax({
        type: "POST",
        crossDomain: true,
        dataType: 'jsonp',
        jsonpCallback: "func",
        url: "http://tracker.myfashionmarketer.com/myfashion/loginService.action?username=" + id + "&password=" + puid + "&callback=?",
        contentType: "application/json; charset=utf-8",
        //   contentType: "application/json",
        success: function (json) {
            if (json.code === 101) {

                window.location = "/home/" + module;

            }
        },
        error: function (e) {
            console.log("abc");
            console.log(e);
        }
    })
})
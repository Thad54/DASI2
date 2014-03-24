function ShowForm(){
	$("#register_form").fadeIn("slow");
}

function ShowAvatarForm(){
	$("#register_Avatarform").fadeIn("slow");
}

function checkPassword(e){
	var pass = document.getElementById("password").value;
	if(e.value == pass && pass !=""){
		//alert("right");
		$(".wrong").css("visibility","hidden");
		$(".right").css("visibility","visible");
		passform = true;
	} else {
		//alert("wrong");
		$(".right").css("visibility","hidden");
		$(".wrong").css("visibility","visible");
		passform = false;
	}
}
function ValidateForm()
{
    var name = document.ContactForm.RegisterName;
	var surname = document.ContactForm.RegisterSurname;
	var username = document.ContactForm.RegisterUsername;
    var email = document.ContactForm.registerEmail;
	var file = document.ContactForm.files;
	
	if(username.indexof("/") != -1 || username.indexof("\\") != -1){
		window.alert("The username can not contain any \"/\" or \"\\\"");
	}
    if (name.value == "" || surname.value == "" || username == "" || email == "" || passform == false || file.value == "")
    {
        window.alert("All Fields are required");
        return false;
    }
    return true;
}

$(".editable").keypress(function(e){
	if(e.which == 13){
		//$.get("index.php",{Original : "images/Test.png", Rename : "images/working.png"});
		var newName = this.innerHTML;
		if(newName.indexOf("jpg") !== -1 || newName.indexOf("jpeg") !== -1 || newName.indexOf("JPG") !== -1 || newName.indexOf("JPEG") !== -1 || newName.indexOf("gif") !== -1 || newName.indexOf("GIF") !== -1 || newName.indexOf("png") !== -1 || newName.indexOf("PNG") !== -1 ){
		//alert("Id = "+this.id+"; new name = "+this.innerHTMl);
		var stub = this.id.split("/", 2);
		newName = "./"+stub[1]+"/"+this.innerHTML;
		//alert("index.php?Original="+this.id+"&Rename=images/"+this.innerHTML);
		window.location = "index.php?Original="+this.id+"&Rename="+newName;
		} else {
			alert("Only jpg, jpeg, png and gif are allows as file extensions.");
		}
		
		//window.location.reload();
		return false
	}
	});
	
	
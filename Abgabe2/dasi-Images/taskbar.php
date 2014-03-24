<div id="background"></div>
		<a id="avatarButton" href="javascript:void()" onclick="ShowAvatarForm()"><span id="showA">Upload new Avatar</span></a>
			<div id="register_Avatarform">
				<form action="changeAvatar.php" method="Post" name="ContactForm" id="reg_form" action="post" enctype="multipart/form-data">
					<p>Avatar</p>
					<input type="hidden" name="MAX_FILE_SIZE" value="1024000">
					<input id="dropzone" type="file" name="files" >
					<input type="submit" value="submit">
				</form>	
			</div>

<div id="uploader">
	Upload Image
	<form action="image-upload.php" method="Post" name="ImageForm" id="img_form" action="post" enctype="multipart/form-data">
		<input type="hidden" name="MAX_FILE_SIZE" value="1024000">
		<input id="dropzone" type="file" name="files" >
		<input type="submit" value="submit">	
	</form>					
</div>					
<div id="User">

<?php
	$host   = 'localhost'; 
		$dbUser = 'root'; 
		$dbPass = ''; 
		$dbName = 'webtop'; 
		$dbConn = mysql_connect($host, $dbUser, $dbPass)
					  or trigger_error(mysql_error(), E_USER_ERROR);
		mysql_select_db($dbName, $dbConn);
		$username = $_SESSION["Name"];
		$sql = "SELECT `nachname` FROM `user` WHERE username = '$username';";
		$check = mysql_query($sql, $dbConn) or die(mysql_error());
		$check = mysql_fetch_array($check);
		if($check != ""){
		$sql = "SELECT `picture` FROM `user` WHERE username = '$username';";
		$check = mysql_query($sql, $dbConn) or die(mysql_error());
		$check = mysql_fetch_assoc($check, MYSQL_ASSOC);
		foreach ($check as $name => $age){}
		if($age != ""){
		echo "<img style='height:60px; border:1px solid rgb(94, 94, 94); border-radius:4px;' src='".$age."'>";
		}else{
		echo "<img style='height:60px; border:1px solid rgb(94, 94, 94); border-radius:4px;' src='art assets/images/dancies_140_transp.png'>";
		}
		}

	if(isset($_COOKIE["User"])){
	echo $_COOKIE["User"]; 
	} else {
	echo $_SESSION["Name"];
	}
?>

<a href="logout.php"><img onclick="logout()" src="art assets/images/logout.png"></a>
</div>
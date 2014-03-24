<?php
	session_start();
	
	$subdir = "./".$_POST['RegisterUsername']."/";	
	
	if(!mkdir($subdir)){
		echo 'An Error occured please try again';
	}
	
	$fileHandle = opendir($subdir);
	if (isset($_FILES['files'])) {
						$fileupload=$_FILES['files'];
						if ( !$fileupload['error'] 								
							&& $fileupload['size']>0							
							&& $fileupload['tmp_name']							
							&& is_uploaded_file($fileupload['tmp_name']))		
							  move_uploaded_file($fileupload['tmp_name'],$subdir.$fileupload['name']);
							else echo 'An Error occured please try again';
}

	$host   = 'localhost'; 
	$dbUser = 'root'; 
	$dbPass = ''; 
	$dbName = 'webtop'; 
		
		$db = @new mysqli($host, $dbUser, $dbPass, $dbName);
		$sql = "CREATE TABLE IF NOT EXISTS user (
		username varchar(32) PRIMARY KEY,
		pwd varchar(32),
		vorname varchar(32),
		nachname varchar(32),
		email varchar(64),
		picture varchar(128)
		);";
		$db->query($sql);
		$username = $_POST['RegisterUsername'];
		$name = $_POST['RegisterName'];
		$surname = $_POST['RegisterSurname'];
		$password = md5($_POST['RegisterPassword']);
		$mail = $_POST['RegisterEmail'];
		if($fileupload['name'] != ""){
			$image = $subdir.$fileupload['name'];
		} else {
			$image = "art assets/images/dancies_140_transp.png";
		}
		$sql = "SELECT `nachname` FROM `user` WHERE username = ?;";
		$result = $db->prepare($sql);
		$result->bind_param('s', $username);
		$result->execute();
		$result->bind_result($check);
		$check = $result->fetch();
		$result->close();
		
		if($check == ""){
		$sql = "INSERT INTO user (username, pwd, vorname, nachname, email, picture) 
			VALUES ('$username', '$password', '$name', '$surname', '$mail', '$image');";
		$sql = "INSERT INTO user (username, pwd, vorname, nachname, email, picture) 
			VALUES (?, ?, ?, ?, ?, ?);";
		$result = $db->prepare($sql);
		$result->bind_param('ssssss', $username, $password, $name, $surname, $mail, $image);
		$result->execute();
		$result->close();
	$_SESSION['Name'] = htmlspecialchars($username);
	$_SESSION['login'] = 1;
	header("location: index.php");
	$db->close();
	} else {
	$_SESSION["error"] = "User already exists!";
	
	header("location: index.php");
	}


?>
<?php
	session_start();
	
	if(isset($_COOKIE["User"])){
		$username = $_COOKIE["User"]; 
		} else {
		$username = $_SESSION["Name"];
		}
	
	$subdir = "./".$username."/";	
	
	$fileHandle = opendir($subdir);
	$fileupload=$_FILES['files'];
	if ( !$fileupload['error'] 								
		&& $fileupload['size']>0							
		&& $fileupload['tmp_name']							
		&& is_uploaded_file($fileupload['tmp_name']))		
		move_uploaded_file($fileupload['tmp_name'],$subdir.$fileupload['name']);
	else echo 'An Error occured please try again';
	$filename = $subdir.$fileupload['name'];
	
	$host   = 'localhost'; 
	$dbUser = 'root'; 
	$dbPass = ''; 
	$dbName = 'webtop'; 
		
		$db = @new mysqli($host, $dbUser, $dbPass, $dbName);

		$sql = "UPDATE user SET picture = ? where username = ?;";
		$result = $db->prepare($sql);
		$result->bind_param('ss', $filename, $username);
		$result->execute();
		$result->close();
		
	$db->close();	
	header("location: index.php");


?>
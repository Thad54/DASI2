<?php
	session_start();

	if(isset($_COOKIE["User"])){
		$subdir = "./".$_COOKIE["User"]."/"; 
	} else {
		$subdir = "./".$_SESSION["Name"]."/";
	}
	
	echo $subdir;
	
	$fileHandle = opendir($subdir);
	if (isset($_FILES['files'])) {
						$fileupload=$_FILES['files'];
						if($fileupload['type'] == 'image/jpeg' || $fileupload['type'] == 'image/png' || $fileupload['type'] == 'image/gif'){
						if ( !$fileupload['error'] 								
							&& $fileupload['size']>0							
							&& $fileupload['tmp_name']							
							&& is_uploaded_file($fileupload['tmp_name']))		
							  move_uploaded_file($fileupload['tmp_name'],$subdir.$fileupload['name']);
							else echo 'An Error occured please try again';
	} else {
		$_SESSION["Error"] = "Only images are allowed(jpeg, png, gif)";
	}
	 }
	header("location: index.php");
?>


<!DOCTYPE html>
<html>
<head>
	<title>Webtop</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<link rel="stylesheet" type="text/css" href="art assets/stylesheet.css"/>
	</head>
<body unselectable="on">
<?php
	//error_reporting(0);
	session_start();
	
	if(isset($_COOKIE["User"])){
		$subdir = "./".$_COOKIE["User"]."/"; 
		$fileHandle = opendir($subdir);
	} else if(isset($_SESSION["Name"])){
		$subdir = "./".$_SESSION["Name"]."/";
		$fileHandle = opendir($subdir);
	}
	
	if(isset($_GET['delete']) && $_GET['delete']){
		unlink($_GET['delete']);
		unset($_GET['delete']);
		header("location: index.php");
	}
	
		if(isset($_GET['Rename'])){		
			rename($_GET['Original'], $_GET['Rename']);
			unset($_GET['Rename']);
			header("location: index.php");
		}
		if(isset($_POST['Edit_Name'])){
		$_SESSION['Edit_Name'] = $_POST['Edit_Name'];
		}
		if(isset($_SESSION['save'])){
			unset($_SESSION['save']);
			echo "<script>alert('save');</script>";
			Header('Location: '.$_SERVER['PHP_SELF']);
			Exit();
		}
		include("login.php");
	if(isset($_POST["Name"]) && isset($_POST["Password"])){
			$login = authenticate($_POST["Name"],$_POST["Password"]);
			if($login == false){
				$error = "Wrong Login Information";
			}else if($login = true){
				$_SESSION["Name"] = htmlspecialchars($_POST["Name"]);
				$error = "";
				$_SESSION["login"] = 1;
				$_SESSION["Name"] = htmlspecialchars($_POST["Name"]);
				setcookie ("User", "", time()-1800);
				header("Location:index.php");
			}
		}else{
			$_SESSION["login"] = 0;
			$error = "";
		}
		if((isset($_FILES['file'])) || (isset($_SESSION["Name"]))){
			$_SESSION["login"] = 1;
		}
	if(($_SESSION["login"] == 1) || (isset($_COOKIE["User"])) || (isset($_SESSION["upload"]))){
	
		echo "<div id='taskbar'>";
			include("taskbar.php");
		echo "</div>";
		if(isset($_SESSION["Error"])){
			echo "<script>alert('".$_SESSION["Error"]."');</script>";
			unset($_SESSION["Error"]);
		}
		
		echo "<div id='desktop'>";	
		while($myFile = readdir($fileHandle)){
					if($myFile != "." && $myFile != ".."){
						//$type = getimagesize($myFile);
						echo "<div class='image'>";
						echo "<a class='fancybox-thumb' rel='fancybox-thumb' href='".$subdir.$myFile."'>";
						echo "<img class='picture' src='".$subdir.$myFile."' alt='' />";
						echo "</a>";
						echo "<a href='index.php?delete=".$subdir.$myFile."'><img class='img' src='art assets/images/icon_close.png'/></a>";
						echo "<a href='download.php?Name=".$myFile."&Path=".$subdir.$myFile."&Type=".$myFile['type']."'><img class='img' src='art assets/images/download.png'/></a>";						
						echo "<p contenteditable='true' id=".$subdir.$myFile." class='editable'>".$myFile."</p>";
						echo "</div>";
		}	
	}
		
	//	echo "<script> alert('working so far')</script>";
		echo "</div>";
	}else{
		echo "<div id='loginScreen'>";
		include("form.php");
		if(isset($_SESSION['error'])){$error = $_SESSION['error'];}
		echo "<p id='error' style='color:red;'> $error </p>";
		if(isset($_SESSION['error'])){unset($_SESSION['error']);}
		echo "</div>";
	}
?>
<script type="text/javascript" src="scripts/jquery-1.11.0.js"></script>
	<script type="text/javascript" src="scripts/dropzone.js"></script>
	<script type="text/javascript" src="scripts/loginForm.js"></script>
</body>
</html>
﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="ASP.NET_BlackJack.Test" %>

<!DOCTYPE html>
<html lang="en">
  <head>
		<!-- Required meta tags -->
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta name="description" content="Modesta Naciute blackjack card game Javascript project">
		<meta name="keywords" content="Blackjack, card game, coding, vanilla Javascript, modules, hanhaechi, project, github, bootcamp">
		<meta name="author" content="Modesta Naciute">
		<link rel="icon" href="img/favicon.ico" type="image/x-icon">

		<title>Mod's Blackjack</title>

		<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
		<link rel='stylesheet' href='https://cdn.jsdelivr.net/font-hack/2.020/css/hack.min.css'>
		<link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">
		<link rel="stylesheet" href="css/bootstrap4-neon-glow.min.css">
		<link rel="stylesheet" href="css/bj.css">

  </head>
  <body>

  
  <div class="container text-mono">
    <div class="pt-5">
      <h1 style="margin-left: 440px"> blackjack<span class="vim-caret">.</span></h1>
      <div class="card mt-4 text-center">
				<div class="card-body">
					Checking if an unfinished game exists on the server. Please grab a coffee <span class="fa fa-coffee"> </span>
				</div>
			</div>
      <hr>
			
			<div class="dealer">
				<h6> dealer : <span class="dealer-count">~</span></h6>
				<div class="dealer-cards">	
				</div>
			</div>
			
			<hr>
			
			<div class="player pb-4">
				<h6> you : <span class="player-count">~</span></h6>
				<div class="player-cards">
				</div>
			</div>
			
    </div>
  </div>
  <div class="container text-mono">
 	 <div class="buttons pb-3">
			<button id="deal" class="btn btn-primary btn-shadow px-3 my-2 ml-0">deal the cards</button>
			<button id="hit" class="btn btn-outline-primary btn-shadow px-3 my-2 ml-0">hit</button>
			<button id="stand" class="btn btn-outline-primary btn-shadow px-3 my-2 ml-0">stand</button>
		</div>
		
		<footer class="pt-1"> 
			<small class="text-muted"> &copy; Concept, code (JS), card modifications: <a href="https://www.linkedin.com/in/modesta/" target="_blank">Modesta Naciute</a>, theme: <a href="https://hackerthemes.com/bootstrap-themes/neon-glow/" target="_blank">hackerthemes</a>, original cards: <a href="https://www.sketchappsources.com/free-source/3060-cards-deck-template-sketch-freebie-resource.html" target="_blank">Grafik_fighter</a> </small> 
		</footer>
		
		
  </div>

  


    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="casino.js"></script>
		<script src="blackjack.js"></script>
		<script src="cards.js"></script>
		<script src="app.js"></script>
  </body>
</html>

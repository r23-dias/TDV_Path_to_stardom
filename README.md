# Path to Stardom TDV

## Identificação do Grupo

- [João Miguel dos Santos Veloso] - [31492]
- [Nuno Gonçalves Soares ] - [33220]
- [Rodrigo Vieira Dias] - [31081]
  
## Descrição do Projeto

Path to Stardom TDV é um jogo de luta 2D desenvolvido em C# utilizando o framework MonoGame em Microsoft Visual Studio 2022. O jogo apresenta combates entre dois lutadores com sistema de vida, controlos responsivos e interface gráfica personalizada. 

## Implementação

### Arquitetura do Jogo

O projeto foi estruturado seguindo uma arquitetura modular com separação clara de responsabilidades:

- **Game1.cs**: Classe principal que gere os estados do jogo (Menu Principal, Jogo, Créditos)
- **MenuSystem.cs**: Sistema de menus com navegação por teclado
- **Fighter.cs**: Classe dos personagens lutadores com sistema de combate
- **StaticBackground.cs**: Sistema de background
- **BitmapFont.cs**: Sistema de fonte bitmap personalizada para renderização de texto
- **Camera.cs**: Sistema de controlo da câmera adaptativa

### Decisões de Design

1. **Sistema de Estados**: Implementação de uma máquina de estados simples para gerenciar transições entre menu, jogo e créditos.
2. **Fonte Bitmap Personalizada**: Criação de um sistema de fonte próprio usando arrays booleanos para maior controlo visual.
3. **Background Dinâmico**: Implementação de parallax scrolling com múltiplas camadas para criar profundidade visual.
4. **Sistema de Combate**: Mecânicas de luta com detecção de colisões e sistema de vida tal como ataques.
5. **Interface Responsiva**: Menus navegáveis com indicadores visuais de seleção.

### Características Técnicas

- **Framework**: MonoGame (.NET 6.0/8.0)
- **Resolução**: 1280x720 (Fullscreen)
- **Gráficos**: Renderização 2D com SpriteBatch
- **Input**: Suporte completo para teclado
- **Estados**: Menu Principal, Gameplay, Tela de Créditos

## Instruções de Jogo

### Controlos

- **Menu Principal**: 
  - Setas ↑/↓ para navegar
  - Enter para selecionar
  - ESC para sair
- **Durante o Jogo**:
  - ESC para voltar ao menu principal

  Player1:
  - WASD para controlo de movimento
  - F/G/H para reproduzir diferentes ataques

    Player2:
  - SETAS ↑/↓ para controlo de movimento
  - 1/2/3 para reproduzir diferentes ataques

### Como Jogar
1. Execute o jogo
2. No menu principal, selecione "NOVO JOGO" para iniciar uma partida entre 2 lutadores
3. Escolha "CRÉDITOS" para ver informações sobre o desenvolvimento e os programadores do Software
4. Use "SAIR" para fechar o jogo

### Objetivo
Derrotar o adversário reduzindo a sua barra de vida a zero através de ataques estratégicos.

### Créditos
Sprites: https://luizmelo.itch.io/

## Requisitos do Sistema

- .NET 6.0 ou superior
- MonoGame Framework
- Sistema operacional compatível com MonoGame (Windows, Linux, macOS)



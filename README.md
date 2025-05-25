# Path to Stardom TDV

**Grupo TDV**
- [Nuno ] - [Número do Aluno 1]
- [Nome do Aluno 2] - [Número do Aluno 2]
- [Nome do Aluno 3] - [Número do Aluno 3]
- 
## Descrição do Projeto

Path to Stardom TDV é um jogo de luta 2D desenvolvido em C# utilizando o framework MonoGame. O jogo apresenta combates entre dois lutadores com sistema de vida, controles responsivos e interface gráfica personalizada.

## Implementação

### Arquitetura do Jogo

O projeto foi estruturado seguindo uma arquitetura modular com separação clara de responsabilidades:

- **Game1.cs**: Classe principal que gerencia os estados do jogo (Menu Principal, Jogo, Créditos)
- **MenuSystem.cs**: Sistema de menus com navegação por teclado
- **Fighter.cs**: Classe dos personagens lutadores com sistema de combate
- **DynamicBackground.cs**: Sistema de fundo dinâmico com parallax scrolling
- **BitmapFont.cs**: Sistema de fonte bitmap personalizada para renderização de texto

### Decisões de Design

1. **Sistema de Estados**: Implementação de uma máquina de estados simples para gerenciar transições entre menu, jogo e créditos
2. **Fonte Bitmap Personalizada**: Criação de um sistema de fonte próprio usando arrays booleanos para maior controle visual
3. **Background Dinâmico**: Implementação de parallax scrolling com múltiplas camadas para criar profundidade visual
4. **Sistema de Combate**: Mecânicas de luta com detecção de colisões e sistema de vida
5. **Interface Responsiva**: Menus navegáveis com indicadores visuais de seleção

### Características Técnicas

- **Framework**: MonoGame (.NET 6.0/8.0)
- **Resolução**: 1280x720 (Fullscreen)
- **Gráficos**: Renderização 2D com SpriteBatch
- **Input**: Suporte completo para teclado
- **Estados**: Menu Principal, Gameplay, Tela de Créditos

## Instruções de Jogo

### Controles
- **Menu Principal**: 
  - Setas ↑/↓ para navegar
  - Enter para selecionar
  - ESC para sair
- **Durante o Jogo**:
  - ESC para voltar ao menu principal
  - [Controles específicos dos lutadores a serem definidos]

### Como Jogar
1. Execute o jogo
2. No menu principal, selecione "NOVO JOGO" para iniciar uma partida
3. Escolha "CRÉDITOS" para ver informações sobre o desenvolvimento
4. Use "SAIR" para fechar o jogo

### Objetivo
Derrote o oponente reduzindo sua barra de vida a zero através de combos e ataques estratégicos.

## Identificação do Grupo



## Requisitos do Sistema

- .NET 6.0 ou superior
- MonoGame Framework
- Sistema operacional compatível com MonoGame (Windows, Linux, macOS)



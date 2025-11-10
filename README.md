# PayFlow API

# API REST para processamento de pagamentos via múltiplos provedores (FastPay e SecurePay), com seleção automática de provedor, cálculo de taxas e resposta padronizada.

# 

# 🏗️ Arquitetura

# 🔹 Padrões e Tecnologias

# ASP.NET Core 6+

# 

# Injeção de Dependência (DI) com Scoped e Singleton

# 

# Controllers REST com \[ApiController]

# 

# System.Text.Json para serialização

# 

# 🔹 Camadas

# Camada	Responsabilidade

# Controllers	Recebe requisições HTTP e retorna respostas

# Models	Define os contratos de entrada e saída

# Providers	Implementa lógica de integração com provedores

# Factory	Seleciona o provedor adequado com fallback

# Tests	Valida lógica de negócio e integração

# 🔹 Provedores

# FastPayProvider: usa taxa de 3,49%

# 

# SecurePayProvider: usa taxa de 2,99% + R$0,40

# 

# Seleção automática com fallback em caso de falha

# 

# 🚀 Como rodar com Docker

# 1\. Pré-requisitos

# Docker instalado

# 

# .NET SDK (opcional para desenvolvimento local)

# 

# 2\. Build da imagem

# bash

# docker build -t payflow-api .

# 

# 3\. Rodar o container

# bash

# docker run -d -p 8080:80 payflow-api



# 4\. Testar a API

# bash

# curl -X POST http://localhost:8080/payments \\

# &nbsp; -H "Content-Type: application/json" \\

# &nbsp; -d '{"amount": 120.50, "currency": "BRL"}'

# ✅ Resposta esperada

# json

# {

# &nbsp; "id": 1,

# &nbsp; "externalId": "SP-19283",

# &nbsp; "status": "approved",

# &nbsp; "provider": "SecurePay",

# &nbsp; "grossAmount": 120.50,

# &nbsp; "fee": 4.01,

# &nbsp; "netAmount": 116.49

# }


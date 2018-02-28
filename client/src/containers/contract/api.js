import axios from 'axios'

import { ROOT_URL } from 'actions/environment'

const API = 'api/Contracts';

export function fetchContracts() {
  const request = axios.get(`${ROOT_URL}${API}`)
  return request
}

export function fetchContract(id) {
  const request = axios.get(`${ROOT_URL}${API}/${id}`)
  return request
}

export function createContract(props) {
  const request = axios.post(`${ROOT_URL}${API}`, props)
  return request
}

export function updateContract(props) {
  const request = axios.put(`${ROOT_URL}${API}/${props.id}`, props)
  return request
}

export function deleteContract(id) {
  const request = axios.delete(`${ROOT_URL}${API}/${id}`)
  return request
}
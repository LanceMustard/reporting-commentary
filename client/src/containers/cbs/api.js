import axios from 'axios'

import { ROOT_URL } from 'actions/environment'

const API = 'api/CBS';

export function fetchCBSs(id) {
  const request = axios.get(`${ROOT_URL}api/ContractCBS/${id}`)
  return request
}

export function fetchCBS(id) {
  const request = axios.get(`${ROOT_URL}${API}/${id}`)
  return request
}

export function createCBS(props) {
  const request = axios.post(`${ROOT_URL}${API}`, props)
  return request
}

export function updateCBS(props) {
  const request = axios.put(`${ROOT_URL}${API}/${props.id}`, props)
  return request
}

export function deleteCBS(id) {
  const request = axios.delete(`${ROOT_URL}${API}/${id}`)
  return request
}